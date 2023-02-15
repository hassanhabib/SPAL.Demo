using Xeptions;

namespace Standard.SPAL.Storage.Abstractions.Models.Exceptions
{
    public class ProviderValidationException : Xeption
    {
        public ProviderValidationException(Xeption innerException)
            : base(message: "Provider validation errors occurred, please try again.", innerException)
        { }
    }
}
