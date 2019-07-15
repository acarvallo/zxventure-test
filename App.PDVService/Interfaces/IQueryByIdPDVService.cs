using App.Core;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.PDVService.Interfaces
{
    public interface IQueryByIdPDVService : IService<int, PDVEntity>
    {
    }
}
