using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core;
using App.PDVService;
using App.PDVService.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace ZXVenture.PDVS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDVController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "PDV Api is available." };
        }


        [HttpPost]
        [Route("GetById")]
        public async Task<ActionResult<ResultObject<PDVEntity>>> GetById([FromBody] RequestObject<int> request)
        {
            var app = AppPDV.Create();
            ResultObject<PDVEntity> ret = null;
            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                ret = await app.GetService<IQueryByIdPDVService>().Run(request);
            }
            return ret;
        }

        [HttpPost]
        [Route("Nearst")]
        public async Task<ActionResult<ResultObject<PDVEntity>>> Nearst([FromBody]  RequestObject<double[]> request)
        {
            var app = AppPDV.Create();
            ResultObject<PDVEntity> ret = null;
            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                ret = await app.GetService<IQueryNearstPDVService>().Run(request);
            }
            return ret;
        }
        [HttpPost]
        [Route("New")]
        public async Task<ActionResult<ResultObject<PDVEntity>>> New([FromBody] RequestObject<PDVEntity> request)
        {
            var app = AppPDV.Create();
            ResultObject<PDVEntity> ret = null;
            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                ret = await app.GetService<ICreatePDVService>().Run(request);
            }
            return ret;
        }


    }
}
