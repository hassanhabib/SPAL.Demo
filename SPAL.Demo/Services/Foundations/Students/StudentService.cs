using SPAL.Demo.Brokers;
using SPAL.Demo.Models.Students;

namespace SPAL.Demo.Services.Foundations.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(
            IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public ValueTask<Student> AddStudentAsync(Student student) =>
            TryCatch(async () =>
            {
                ValidateStudentOnAdd(student);

                return await this.storageBroker.InsertStudentAsync(student);
            });

        public IQueryable<Student> RetrieveAllStudents() =>
            TryCatch(() => this.storageBroker.SelectAllStudents());

        public ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId) =>
            TryCatch(async () =>
            {
                ValidateStudentId(studentId);

                Student maybeStudent = await this.storageBroker
                    .SelectStudentByIdAsync(studentId);

                ValidateStorageStudent(maybeStudent, studentId);

                return maybeStudent;
            });

        public ValueTask<Student> ModifyStudentAsync(Student student) =>
            TryCatch(async () =>
            {
                ValidateStudentOnModify(student);

                Student maybeStudent =
                    await this.storageBroker.SelectStudentByIdAsync(student.Id);

                ValidateStorageStudent(maybeStudent, student.Id);

                return await this.storageBroker.UpdateStudentAsync(student);
            });

        public ValueTask<Student> RemoveStudentByIdAsync(Guid studentId) =>
            TryCatch(async () =>
            {
                ValidateStudentId(studentId);

                Student maybeStudent = await this.storageBroker
                    .SelectStudentByIdAsync(studentId);

                ValidateStorageStudent(maybeStudent, studentId);

                return await this.storageBroker.DeleteStudentAsync(maybeStudent);
            });
    }
}