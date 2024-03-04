using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Repos
{
    public class EducationLevelRepo<TDbContext> : RepositoryBase<TDbContext, EducationLevel>, IEducationLevelRepo
        where TDbContext : DbContext
    {
        public EducationLevelRepo(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public IQueryable<EducationLevel> SelectAllIncluded()
        {
            return FindAll().Include(educationLevel => educationLevel.Students);
        }

        public ICollection<Student> SelectStudentsBy(Guid educationLevelId)
        {
            EducationLevel? resultEducationLevel=
                FindByCondition(educationLevel => educationLevel.Id == educationLevelId)
                .Include(educationLevel => educationLevel.Students)
                .FirstOrDefault();
            if (resultEducationLevel != null && resultEducationLevel.Students is not null)
                return resultEducationLevel.Students;
            else
                return new List<Student>();
        }
    }
}
