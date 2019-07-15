using App.Core;
using App.PDVService.Interfaces;
using Data.Entities;
using Data.UnifOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils.Extensions;

namespace App.PDVService
{
    public class CreatePDVService : ICreatePDVService
    {
        IUnitOfWork UnitOfWork { get; }

        public CreatePDVService(IAppContext app, IUnitOfWork uow)
        {
            UnitOfWork = uow;
            AppContext = app;
        }

        public IAppContext AppContext { get; }

        public async Task<ResultObject<PDVEntity>> Run(RequestObject<PDVEntity> request)
        {
            var ret = new ResultObject<PDVEntity>();

            try
            {
                if (await ValidatePDV(request.Data, ret))
                {
                    await UnitOfWork.PDVS.Add(request.Data);
                    ret.Data = request.Data;
                    ret.Message = "PDV sucessfully created";
                }
            }
            catch (Exception ex)
            {
                ret.ErrorMessage = ex.ToString();
            }
            return ret;
        }

        public async Task<bool> ValidatePDV(PDVEntity pdv, ResultObject<PDVEntity> ret)
        {
            if (string.IsNullOrEmpty(pdv.ownerName)
                  || string.IsNullOrEmpty(pdv.tradingName)
                  || string.IsNullOrEmpty(pdv.document)
                  || pdv.address == null || pdv.coverageArea == null)
            {
                ret.ErrorMessage = "All the filed are required.";
                return false;
            }

            if (!pdv.document.IsCnpj())
            {
                ret.ErrorMessage = "Document is invalid.";
                return false;
            }

            pdv.document = pdv.document.CnpJOnlyNumbers();

            var find = await UnitOfWork.PDVS.Find(p => p.document == pdv.document);

            if (find.Any())
            {
                ret.ErrorMessage = "Document number already exists.";
                return false;
            }

            if (pdv.address.coordinates == null || pdv.address.coordinates.Length < 2 || string.IsNullOrEmpty(pdv.address.type) || pdv.address.type != "Point")
            {
                ret.ErrorMessage = "Address format invalid.";
                return false;
            }

            if (pdv.coverageArea.coordinates == null
                || pdv.address.coordinates.Length < 2
                || pdv.coverageArea.coordinates.Any(p => p.Any(p2 => p2.Length < 3))
                || pdv.coverageArea.coordinates.Any(p => p.Any(p2 => p2.Any(p3 => p3.Length < 2)))
                || string.IsNullOrEmpty(pdv.coverageArea.type)
                || pdv.coverageArea.type != "MultiPolygon")
            {
                ret.ErrorMessage = "Covarage area format invalid.";
                return false;
            }
            return true;
        }
    }
}
