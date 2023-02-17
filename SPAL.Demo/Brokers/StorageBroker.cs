using Standard.SPAL.Storage.Abstractions;

namespace SPAL.Demo.Brokers
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly IStorageSpal storageAbstractProvider;

        public StorageBroker(IStorageSpal storageAbstractProvider) =>
            this.storageAbstractProvider = storageAbstractProvider;
    }
}
