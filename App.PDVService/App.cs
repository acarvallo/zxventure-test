using App.Core;
using Data.Context.MongoDB;
using Data.Repository.Core;
using Data.UnifOfWork;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Linq;
using System.Reflection;

namespace App.PDVService
{
    public class AppPDV : IAppContext
    {
        IConfigurationRoot rootConfig;

        private AppPDV()
        {
            Container = new Container();
            Init();
            InitContainer();
        }

        public static IAppContext Create()
        {
            return new AppPDV();
        }
        private void Init()
        {
            rootConfig = new ConfigurationBuilder()
                  .AddEnvironmentVariables()
                   .AddJsonFile($"appsettings.json", optional: false)
                  .Build();

        }
        private void InitContainer()
        {
            ContextConfig config = rootConfig.GetSection("MongoDBConfig").Get<ContextConfig>();
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            Container.Register<IAppContext>(() => this, Lifestyle.Scoped);
            Container.Register(() =>
            {
                return new MongoDBDataContext(config);

            }, Lifestyle.Scoped);

            RegisterTypes<IUnitOfWork>();
            RegisterTypes<IService>();
            RegisterTypes<IRepositoryAsync>();

            Container.Verify();
        }



        private void RegisterTypes<TInterface>()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = Container.GetTypesToRegister(typeof(TInterface), assemblies)
                 .Where(type => type.IsPublic);

            foreach (var type in types)
            {
                Container.Register(type.GetInterfaces().Where(p => !p.IsGenericType).FirstOrDefault(), type, Lifestyle.Scoped);

            }
        }

        public Container Container { get; }

        public string GetConfig(string key)
        {
            return rootConfig.GetValue<string>(key);
        }

        public T GetService<T>() where T : class, IService
        {
            return Container.GetInstance<T>();
        }
    }
}
