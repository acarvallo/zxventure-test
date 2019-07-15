using Data.Repository.MongoDB;

namespace Data.UnifOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public IPDVRepository PDVS { get; }

        public UnitOfWork(IPDVRepository pdvs)
        {
            PDVS = pdvs;
        }

    }
}
