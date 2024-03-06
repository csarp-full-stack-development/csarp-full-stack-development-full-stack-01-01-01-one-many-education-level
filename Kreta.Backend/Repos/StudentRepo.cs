using Kreta.Shared.Models.SchoolCitizens;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Repos
{
    public class StudentRepo<TDbContext> : RepositoryBase<TDbContext, Student>, IStudentRepo
        where TDbContext : DbContext
    {
        public StudentRepo(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public IQueryable<Student> SelectAllIncluded()
        {
            return FindAll().Include(student => student.EducationLevel);
        }

        public IQueryable<Student> SelectAllStudentNoEducationLevel()
        {
            return FindAll().Where(student => student.EducationLevelId==null || student.EducationLevelId == Guid.Empty);
        }

        public IQueryable<Student> SelectAllByEducationId(Guid educationId)
        {
            return FindAll().Where(student => student.EducationLevelId==educationId);
        }
    }
}
