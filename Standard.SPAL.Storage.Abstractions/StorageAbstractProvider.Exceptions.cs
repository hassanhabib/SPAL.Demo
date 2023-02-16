using Standard.SPAL.Storage.Abstractions.Models.Exceptions;

namespace Standard.SPAL.Storage.Abstractions
{
    public partial class StorageAbstractProvider
    {
        private delegate ValueTask<T> ReturningObjectFunction<T>();
        private delegate IQueryable<T> ReturningObjectsFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningObjectFunction<T> returningObjectFunction)
            where T : class
        {
            try
            {
                return await returningObjectFunction();
            }
            catch (Exception ex)
                when (
                    ex is ProviderValidationException
                    || ex is ProviderDependencyValidationException
                    || ex is ProviderDependencyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProviderServiceException(ex);
            }
        }

        private IQueryable<T> TryCatch<T>(ReturningObjectsFunction<T> returningObjectsFunction)
            where T : class
        {
            try
            {
                return returningObjectsFunction();
            }
            catch (Exception ex)
                when (
                    ex is ProviderValidationException
                    || ex is ProviderDependencyValidationException
                    || ex is ProviderDependencyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProviderServiceException(ex);
            }
        }
    }
}
