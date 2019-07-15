using App.PDVService;
using BAMCIS.GeoJSON;
using Data.Context.MongoDB;
using Data.Entities;
using Data.Repository.MongoDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utils.Extensions;

namespace InitialDataLoad
{
    public class DataLoad
    {
        public class Data
        {
            public List<PDVEntity> pdvs { get; set; }

        }

        public void LoadData(ContextConfig config, string sourcefile)
        {
            var context = new MongoDBDataContext(config);
            var fileJson = File.ReadAllText(sourcefile);
            var data = JsonConvert.DeserializeObject<Data>(fileJson);

            data.pdvs.ForEach(p => { p.document = p.document.CnpJOnlyNumbers(); });

            context.Database.GetCollection<PDVEntity>("pdvs").InsertMany(data.pdvs);

            Console.WriteLine($"{data.pdvs.Count} itens included in the collection pdvs");

        }
        public async Task getPDV(ContextConfig config)
        {
            var context = new MongoDBDataContext(config);

            var pdv = new PDVRespository(context);

            var ret = await pdv.FindById(1);



        }
    }
}
