using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Core
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int id { get; set; }
    }
}
