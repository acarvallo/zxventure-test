using Data.Repository.MongoDB;

namespace Data.UnifOfWork
{
    public interface IUnitOfWork
    {
        IPDVRepository PDVS { get;}
    }
}
