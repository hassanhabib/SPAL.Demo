using Xeptions;

namespace Standard.SPAL.Storage.Abstractions.Models.Exceptions
{
    public class ProviderServiceException : Xeption
    {
        public ProviderServiceException(Exception innerException)
            : base(
                  message: "Provider service error occurred, contact support.",
                  innerException,
                  data: innerException.Data)
        { }
    }
}
