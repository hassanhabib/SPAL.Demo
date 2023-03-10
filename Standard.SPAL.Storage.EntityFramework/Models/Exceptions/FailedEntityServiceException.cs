using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class FailedEntityServiceException : Xeption
    {
        public FailedEntityServiceException(Exception innerException)
            : base(message: "Failed entity service occurred, please contact support", innerException)
        { }
    }
}