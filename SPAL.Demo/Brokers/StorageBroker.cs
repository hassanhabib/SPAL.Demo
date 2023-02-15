using Standard.SPAL.Storage.Abstractions;

namespace SPAL.Demo.Brokers
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly IStorageAbstractProvider storageAbstractProvider;

        public StorageBroker(IStorageAbstractProvider storageAbstractProvider) =>
            this.storageAbstractProvider = storageAbstractProvider;
    }
}
