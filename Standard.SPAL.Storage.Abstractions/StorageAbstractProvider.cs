namespace Standard.SPAL.Storage.Abstractions
{
    public partial class StorageAbstractProvider : IStorageAbstractProvider
    {
        private readonly IStorageProvider provider;

        public StorageAbstractProvider(IStorageProvider provider)
        {
            this.provider = provider;
        }

        public void Dispose() =>
            this.provider.Dispose();

        public ValueTask<T> InsertAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                return await this.provider.InsertAsync(@object);
            });

        public IQueryable<T> SelectAll<T>() where T : class =>
            TryCatch(() =>
            {
                return this.provider.SelectAll<T>();
            });

        public ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class =>
            TryCatch(async () =>
            {
                return await this.provider.SelectAsync<T>(objectIds);
            });

        public ValueTask<T> UpdateAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                return await this.provider.UpdateAsync(@object);
            });

        public ValueTask<T> DeleteAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                return await this.provider.DeleteAsync(@object);
            });
    }
}
