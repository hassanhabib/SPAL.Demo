using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class LockedEntityException : Xeption
    {
        public LockedEntityException(Exception innerException)
            : base(message: "Locked entity record exception, please try again later", innerException)
        {
        }
    }
}