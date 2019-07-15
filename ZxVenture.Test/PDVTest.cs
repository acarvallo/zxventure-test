using App.Core;
using App.PDVService;
using App.PDVService.Interfaces;
using Data.Entities;
using Data.Entities.Geo;
using Newtonsoft.Json;
using NUnit.Framework;
using SimpleInjector.Lifestyles;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task QueryNearstPDVTest()
        {
            var app = AppPDV.Create();

            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                var request = new RequestObject<double[]>();

                request.Data = new double[] { -46.57421, -21.785741 };
                var ret = await app.GetService<IQueryNearstPDVService>().Run(request);

                Assert.NotNull(ret.Data);

            }

        }
        [Test]
        public async Task QueryByIdTest()
        {
            var app = AppPDV.Create();

            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                var request = new RequestObject<int>();

                request.Data = 31;
                var ret = await app.GetService<IQueryByIdPDVService>().Run(request);

                Assert.NotNull(ret.Data);

            }
        }
        [Test]
        public async Task CreatePDVSameDocument()
        {
            var app = AppPDV.Create();

            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                var request = new RequestObject<PDVEntity>();

                var json = @"{
                        ""id"": 0, 
                        ""tradingName"": ""Nova loja"",
                        ""ownerName"": ""Anderson mendes"",
                        ""document"": ""22745914/0001-14"",
                        ""coverageArea"": { 
                          ""type"": ""MultiPolygon"", 
                          ""coordinates"": [
                        [
                   [
                      [
                         -46.80874,
                         -23.58613
                      ],
                      [
                         -46.83603,
                         -23.62247
                      ],
                      [
                         -46.85234,
                         -23.65691
                      ],
                      [
                         -46.76994,
                         -23.69558
                      ],
                      [
                         -46.7502,
                         -23.69641
                      ],
                      [
                         -46.73046,
                         -23.68906
                      ],
                      [
                         -46.73355,
                         -23.67917
                      ]]]
                          ]
                        },
                        ""address"": { 
                          ""type"": ""Point"",
                          ""coordinates"": [-46.57421, -21.785741]
                        }, 
                    }";

                request.Data = JsonConvert.DeserializeObject<PDVEntity>(json);
                var ret = await app.GetService<ICreatePDVService>().Run(request);

                Assert.IsNull(ret.Data);
                Assert.Equals(ret.ErrorMessage, "Document number already exists.");

            }
        }

        [Test]
        public async Task CreatePDV()
        {
            var app = AppPDV.Create();

            using (AsyncScopedLifestyle.BeginScope(app.Container))
            {
                var request = new RequestObject<PDVEntity>();

                var json = @"{
                        ""id"": 0, 
                        ""tradingName"": ""Nova loja"",
                        ""ownerName"": ""Anderson mendes"",
                        ""document"": ""81.388.424/0001-77"",
                        ""coverageArea"": { 
                          ""type"": ""MultiPolygon"", 
                          ""coordinates"": [
                        [
                   [
                      [
                         -46.80874,
                         -23.58613
                      ],
                      [
                         -46.83603,
                         -23.62247
                      ],
                      [
                         -46.85234,
                         -23.65691
                      ],
                      [
                         -46.76994,
                         -23.69558
                      ],
                      [
                         -46.7502,
                         -23.69641
                      ],
                      [
                         -46.73046,
                         -23.68906
                      ],
                      [
                         -46.73355,
                         -23.67917
                      ]]]
                          ]
                        },
                        ""address"": { 
                          ""type"": ""Point"",
                          ""coordinates"": [-46.57421, -21.785741]
                        }, 
                    }";

                request.Data = JsonConvert.DeserializeObject<PDVEntity>(json);
                var ret = await app.GetService<ICreatePDVService>().Run(request);

                Assert.IsNotNull(ret.Data);
                Assert.Equals(ret.Message, "PDV sucessfully created.");

            }
        }

    }
}