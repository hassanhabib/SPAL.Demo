using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class AlreadyExistsEntityException : Xeption
    {
        public AlreadyExistsEntityException(Exception innerException)
            : base(message: "Entity with the same Id already exists.", innerException)
        { }
    }
}