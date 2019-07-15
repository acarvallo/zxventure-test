using App.Core;
using App.PDVService.Interfaces;
using Data.Entities;
using Data.UnifOfWork;
using System.Threading.Tasks;

namespace App.PDVService
{
    public class QueryNearstPDVService : IQueryNearstPDVService
    {
        IUnitOfWork UnitOfWork { get; }

        public QueryNearstPDVService(IAppContext app, IUnitOfWork uow)
        {
            UnitOfWork = uow;
            AppContext = app;
        }

        public IAppContext AppContext { get; }


        public async Task<ResultObject<PDVEntity>> Run(RequestObject<double[]> request)
        {
            var ret = new ResultObject<PDVEntity>();

            if (request.Data == null || request.Data.Length < 2)
            {
                ret.ErrorMessage = "Missing coordinates";
                return ret;
            }
            
            try
            {
                var pdv = await UnitOfWork.PDVS.GetNearst(request.Data[0], request.Data[1]);
                ret.Data = pdv;
            }
            catch (System.Exception ex)
            {
                ret.ErrorMessage = ex.ToString();
            }
            return ret;
        }
    }
}
