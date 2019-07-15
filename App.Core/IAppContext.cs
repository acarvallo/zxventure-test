using SimpleInjector;
using System;

namespace App.Core
{
    public interface IAppContext
    {

        T GetService<T>() where T : class, IService;
        Container Container { get; }
        string GetConfig(string key);

    }
}
