using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public interface IService
    {
        IAppContext AppContext { get; }
    }
    public interface IService<TRequest, TResult> : IService
    {

        Task<ResultObject<TResult>> Run(RequestObject<TRequest> request);

    }
}
