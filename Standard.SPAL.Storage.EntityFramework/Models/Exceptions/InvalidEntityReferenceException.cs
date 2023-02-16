using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class InvalidEntityReferenceException : Xeption
    {
        public InvalidEntityReferenceException(Exception innerException)
            : base(message: "Invalid entity reference error occurred.", innerException) { }
    }
}