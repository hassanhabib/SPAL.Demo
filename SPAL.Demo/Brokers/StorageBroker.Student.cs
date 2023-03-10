using SPAL.Demo.Models.Students;

namespace SPAL.Demo.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<Student> InsertStudentAsync(Student student) =>
            await this.storageAbstractProvider.InsertAsync(student);

        public IQueryable<Student> SelectAllStudents() =>
            this.storageAbstractProvider.SelectAll<Student>();

        public async ValueTask<Student> SelectStudentByIdAsync(Guid studentId) =>
            await this.storageAbstractProvider.SelectAsync<Student>(studentId);

        public async ValueTask<Student> UpdateStudentAsync(Student student) =>
            await this.storageAbstractProvider.UpdateAsync(student);

        public async ValueTask<Student> DeleteStudentAsync(Student student) =>
            await this.storageAbstractProvider.DeleteAsync(student);
    }
}
