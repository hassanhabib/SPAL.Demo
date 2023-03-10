using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class NotFoundEntityException : Xeption
    {
        public NotFoundEntityException(Guid id)
            : base(message: $"Couldn't find entity with Id: {id}.")
        { }
    }
}