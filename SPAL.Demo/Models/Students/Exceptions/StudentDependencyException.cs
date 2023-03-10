using Xeptions;

namespace SPAL.Demo.Models.Students.Exceptions
{
    public class StudentDependencyException : Xeption
    {
        public StudentDependencyException(Xeption innerException) :
            base(message: "Student dependency error occurred, contact support.", innerException)
        { }
    }
}