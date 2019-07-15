using System;

namespace Data.Context.Core
{
    public interface  IDataContext
    {
         ContextConfig Config { get;  }
         void Add<T>(T objecty);


    }
}
