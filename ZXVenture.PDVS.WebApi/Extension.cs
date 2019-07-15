using Microsoft.AspNetCore.Hosting;
using System.ServiceProcess;

namespace ZXVenture.PDVS.WebApi
{
    public static class Extensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new CustomWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
