using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Parameters;

namespace Kreta.Backend.Repos
{
    public interface IStudentRepo : IRepositoryBase<Student>
    {
        public IQueryable<Student> SelectAllIncluded();
        public IQueryable<Student> SelectAllStudentNoEducationLevel();
        public IQueryable<Student> SelectAllByEducationId(Guid educationId);
    }
}
