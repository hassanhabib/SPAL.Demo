using SPAL.Demo.Models.Students;
using SPAL.Demo.Models.Students.Exceptions;
using Standard.SPAL.Storage.Abstractions.Models.Exceptions;
using Xeptions;

namespace SPAL.Demo.Services.Foundations.Students
{
    public partial class StudentService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();
        private delegate IQueryable<Student> ReturningStudentsFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (NullStudentException nullStudentException)
            {
                throw CreateAndLogValidationException(nullStudentException);
            }
            catch (InvalidStudentException invalidStudentException)
            {
                throw CreateAndLogValidationException(invalidStudentException);
            }
            catch (NotFoundStudentException notFoundStudentException)
            {
                throw CreateAndLogValidationException(notFoundStudentException);
            }
            catch (ProviderValidationException providerValidationException)
            {
                throw CreateAndLogDependencyValidationException(providerValidationException);
            }
            catch (ProviderDependencyValidationException providerDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(providerDependencyValidationException);
            }
            catch (ProviderDependencyException providerDependencyException)
            {
                throw CreateAndLogDependencyException(providerDependencyException);
            }
            catch (ProviderServiceException providerServiceException)
            {
                throw CreateAndLogDependencyException(providerServiceException);
            }
            catch (Exception exception)
            {
                var failedStudentServiceException =
                    new FailedStudentServiceException(exception);

                throw CreateAndLogServiceException(failedStudentServiceException);
            }
        }

        private IQueryable<Student> TryCatch(ReturningStudentsFunction returningStudentsFunction)
        {
            try
            {
                return returningStudentsFunction();
            }
            catch (ProviderDependencyException providerDependencyException)
            {
                throw CreateAndLogDependencyException(providerDependencyException);
            }
            catch (Exception exception)
            {
                var failedStudentServiceException =
                    new FailedStudentServiceException(exception);

                throw CreateAndLogServiceException(failedStudentServiceException);
            }
        }

        private StudentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentValidationException =
                new StudentValidationException(exception);

            return studentValidationException;
        }

        private StudentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);

            return studentDependencyException;
        }

        private StudentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentDependencyValidationException =
                new StudentDependencyValidationException(exception);

            return studentDependencyValidationException;
        }

        private StudentDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);

            return studentDependencyException;
        }

        private StudentServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var studentServiceException = new StudentServiceException(exception);

            return studentServiceException;
        }
    }
}