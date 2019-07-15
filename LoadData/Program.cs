using Data.Context.MongoDB;
using InitialDataLoad;
using System;
using System.Linq;

namespace LoadData
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoad load = new DataLoad();
            ContextConfig config = new ContextConfig();

            if (!args.Contains("-c") || !args.Contains("-db") || !args.Contains("-f"))
            {
                Console.WriteLine("Command invalid :  -c [mongodb connectionstring] -db [databasename] -f [Json file full path]");
                Console.ReadKey();
            }
            else
            {
                config.ConnectionString = args[Array.IndexOf(args, "-c") + 1];
                try
                {
                    config.DataBaseName = args[Array.IndexOf(args, "-db") + 1];
                    load.LoadData(config, args[Array.IndexOf(args, "-f") + 1]);

                    Console.ReadKey();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }





        }
    }
}
