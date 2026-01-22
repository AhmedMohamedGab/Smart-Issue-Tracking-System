using System.Text.Json;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public abstract class JsonRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected List<T> _items;

        protected JsonRepository()
        {
            _items = Load();
        }

        protected abstract string FilePath { get; }

        protected List<T> Load()
        {
            if (!File.Exists(FilePath))
                return new List<T>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<T>>(json)
                   ?? new List<T>();
        }

        protected void Save()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
        }

        public virtual void Add(T entity)
        {
            var item = _items.FirstOrDefault(e => e.Id == entity.Id);
            if (item is null)
            {
                _items.Add(entity);
                Save();
            }
        }

        public virtual void Remove(Guid id)
        {
            _items.RemoveAll(e => e.Id == id);
            Save();
        }

        public virtual void Update(T entity)
        {
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _items[index] = entity;
                Save();
            }
        }

        public T? GetById(Guid id)
            => _items.FirstOrDefault(e => e.Id == id);

        public IEnumerable<T> GetAll()
            => _items.AsReadOnly();
    }
}
