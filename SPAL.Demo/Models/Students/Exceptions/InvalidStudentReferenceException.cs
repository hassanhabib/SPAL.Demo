using Xeptions;

namespace SPAL.Demo.Models.Students.Exceptions
{
    public class InvalidStudentReferenceException : Xeption
    {
        public InvalidStudentReferenceException(Exception innerException)
            : base(message: "Invalid student reference error occurred.", innerException) { }
    }
}