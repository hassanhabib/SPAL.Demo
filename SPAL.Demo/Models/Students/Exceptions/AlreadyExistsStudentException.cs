using Xeptions;

namespace SPAL.Demo.Models.Students.Exceptions
{
    public class AlreadyExistsStudentException : Xeption
    {
        public AlreadyExistsStudentException(Exception innerException)
            : base(message: "Student with the same Id already exists.", innerException)
        { }
    }
}