namespace Loyalty.Core.Entities.SeedWork.Interfaces
{
    public interface IRepository<T> 
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
