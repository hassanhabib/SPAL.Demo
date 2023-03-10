using Xeptions;

namespace SPAL.Demo.Models.Students.Exceptions
{
    public class FailedStudentStorageException : Xeption
    {
        public FailedStudentStorageException(Exception innerException)
            : base(message: "Failed student storage error occurred, contact support.", innerException)
        { }
    }
}