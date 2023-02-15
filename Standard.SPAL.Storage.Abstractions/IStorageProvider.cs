namespace Standard.SPAL.Storage.Abstractions
{
    public interface IStorageProvider : IDisposable
    {
        ValueTask<T> InsertAsync<T>(T @object) where T : class;
        IQueryable<T> SelectAll<T>() where T : class;
        ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class;
        ValueTask<T> UpdateAsync<T>(T @object) where T : class;
        ValueTask<T> DeleteAsync<T>(T @object) where T : class;
    }
}
