using Data.Entities;
using Data.Repository.Core;
using System;
using System.Threading.Tasks;

namespace Data.Repository.MongoDB
{
    public interface IPDVRepository :IRepositoryAsync<PDVEntity>
    {
        Task<PDVEntity> GetNearst(double longt, double lat);
    }
}
