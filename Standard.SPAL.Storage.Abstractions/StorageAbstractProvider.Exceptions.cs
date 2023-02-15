using Standard.SPAL.Storage.Abstractions.Models.Exceptions;
using Xeptions;

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
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderValidationException)))
            {
                throw new ProviderValidationException(ex);
            }
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderDependencyValidationException)))
            {
                throw new ProviderDependencyValidationException(ex);
            }
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderDependencyException)))
            {
                throw new ProviderDependencyException(ex);
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
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderValidationException)))
            {
                throw new ProviderValidationException(ex);
            }
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderDependencyValidationException)))
            {
                throw new ProviderDependencyValidationException(ex);
            }
            catch (Xeption ex) when (ex.GetType().IsSubclassOf(typeof(ProviderDependencyException)))
            {
                throw new ProviderDependencyException(ex);
            }
            catch (Exception ex)
            {
                throw new ProviderServiceException(ex);
            }
        }
    }
}
