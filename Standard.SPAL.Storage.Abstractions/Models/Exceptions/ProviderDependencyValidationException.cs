using Xeptions;

namespace Standard.SPAL.Storage.Abstractions.Models.Exceptions
{
    public class ProviderDependencyValidationException : Xeption
    {
        public ProviderDependencyValidationException(Xeption innerException)
            : base(
                  message: "Provider dependency validation occurred, please try again.",
                  innerException,
                  data: innerException.Data)
        { }
    }
}
