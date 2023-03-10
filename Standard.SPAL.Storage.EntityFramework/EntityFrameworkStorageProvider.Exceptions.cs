using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Standard.SPAL.Storage.Abstractions.Models.Exceptions;
using Standard.SPAL.Storage.EntityFramework.Models.Exceptions;
using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework
{
    public partial class EntityFrameworkStorageProvider
    {
        private delegate ValueTask<T> ReturningEntityFunction<T>();
        private delegate IQueryable<T> ReturningEntitiesFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningEntityFunction<T> returningEntityFunction)
        {
            try
            {
                return await returningEntityFunction();
            }
            catch (NullEntityException nullEntityException)
            {
                throw CreateAndLogValidationException(nullEntityException);
            }
            catch (SqlException sqlException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedEntityStorageException);
            }
            catch (NotFoundEntityException notFoundEntityException)
            {
                throw CreateAndLogValidationException(notFoundEntityException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsEntityException =
                    new AlreadyExistsEntityException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsEntityException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidEntityReferenceException =
                    new InvalidEntityReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidEntityReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedEntityException = new LockedEntityException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedEntityException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedEntityStorageException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(exception);

                throw CreateAndLogServiceException(failedEntityServiceException);
            }
        }

        private IQueryable<T> TryCatch<T>(ReturningEntitiesFunction<T> returningEntitysFunction)
        {
            try
            {
                return returningEntitysFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedEntityStorageException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(exception);

                throw CreateAndLogServiceException(failedEntityServiceException);
            }
        }

        private ProviderValidationException CreateAndLogValidationException(Xeption exception)
        {
            var providerValidationException =
                new ProviderValidationException(exception);

            return providerValidationException;
        }

        private ProviderDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var providerDependencyException =
                new ProviderDependencyException(exception);

            return providerDependencyException;
        }

        private ProviderDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var providerDependencyValidationException =
                new ProviderDependencyValidationException(exception);

            return providerDependencyValidationException;
        }

        private ProviderDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var providerDependencyException =
                new ProviderDependencyException(exception);

            return providerDependencyException;
        }

        private ProviderServiceException CreateAndLogServiceException(Xeption exception)
        {
            var providerServiceException =
                new ProviderServiceException(exception);

            return providerServiceException;
        }
    }
}
