using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class FailedEntityStorageException : Xeption
    {
        public FailedEntityStorageException(Exception innerException)
            : base(message: "Failed entity storage error occurred, contact support.", innerException)
        { }
    }
}