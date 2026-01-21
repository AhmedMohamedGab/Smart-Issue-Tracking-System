using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Remove(Guid id);
        void Update(T entity);
        T? GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}
