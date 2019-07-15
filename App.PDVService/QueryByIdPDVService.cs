using App.Core;
using App.PDVService.Interfaces;
using Data.Entities;
using Data.UnifOfWork;
using System;
using System.Threading.Tasks;

namespace App.PDVService
{
    public class QueryByIdPDVService : IQueryByIdPDVService
    {
        IUnitOfWork UnitOfWork { get; }

        public QueryByIdPDVService(IAppContext app, IUnitOfWork uow)
        {
            UnitOfWork = uow;
            AppContext = app;
        }

        public IAppContext AppContext { get; }


        public async Task<ResultObject<PDVEntity>> Run(RequestObject<int> request)
        {
            var ret = new ResultObject<PDVEntity>();

            if (request.Data < 0)
            {
                ret.ErrorMessage = "Missing Id value";
                return ret;
            }

            try
            {
                var pdv = await UnitOfWork.PDVS.FindById(request.Data);
                ret.Data = pdv;
            }
            catch (Exception ex)
            {
                ret.ErrorMessage = ex.ToString();
            }
            return ret;
        }
    }
}
