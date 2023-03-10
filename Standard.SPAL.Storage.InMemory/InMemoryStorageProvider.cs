using Standard.SPAL.Storage.Abstractions;

namespace Standard.SPAL.Storage.EntityFramework
{
    public class InMemoryStorageProvider : IStorageProvider
    {
        private readonly Dictionary<Type, List<object>> _entities = new Dictionary<Type, List<object>>();

        public async ValueTask<T> InsertAsync<T>(T @object) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            var entitiesOfType = GetEntitiesOfType<T>();
            entitiesOfType.Add(@object);

            return await ValueTask.FromResult(@object);
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            var entitiesOfType = GetEntitiesOfType<T>();
            return entitiesOfType.Cast<T>().AsQueryable();
        }

        public async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class
        {
            List<dynamic> entitiesOfType = GetEntitiesOfType<T>();
            dynamic id = @objectIds.FirstOrDefault();
            entitiesOfType = entitiesOfType.Where(e => e.Id == id).ToList();
            T entity = entitiesOfType.FirstOrDefault() as T;

            return entity == null
                ? throw new Exception("Entity not found.")
                : await ValueTask.FromResult(entity);
        }

        public async ValueTask<T> UpdateAsync<T>(T @object) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            List<dynamic> entitiesOfType = GetEntitiesOfType<T>();
            dynamic changedEntity = @object;
            dynamic existingEntity = entitiesOfType.Where(e => e.Id == changedEntity.Id);

            if (existingEntity == null)
            {
                throw new Exception("Entity not found.");
            }

            entitiesOfType.Remove(existingEntity);
            entitiesOfType.Add(changedEntity);

            return await ValueTask.FromResult(@object);
        }

        public async ValueTask<T> DeleteAsync<T>(T @object) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            List<dynamic> entitiesOfType = GetEntitiesOfType<T>();
            dynamic deletedEntity = @object;
            dynamic existingEntity = entitiesOfType.Where(e => e.Id == deletedEntity.Id);

            if (existingEntity == null)
            {
                throw new Exception("Entity not found.");
            }

            entitiesOfType.Remove(existingEntity);

            return await ValueTask.FromResult(@object);
        }

        private List<object> GetEntitiesOfType<T>() where T : class
        {
            var entityType = typeof(T);
            if (!_entities.ContainsKey(entityType))
            {
                _entities[entityType] = new List<object>();
            }

            return _entities[entityType];
        }

        public void Dispose()
        { }
    }
}
