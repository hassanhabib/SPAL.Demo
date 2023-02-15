using Xeptions;

namespace Standard.SPAL.Storage.Abstractions.Models.Exceptions
{
    public class ProviderDependencyException : Xeption
    {
        public ProviderDependencyException(Exception innerException) :
            base(message: "Provider dependency error occurred, contact support.", innerException)
        { }
    }
}
