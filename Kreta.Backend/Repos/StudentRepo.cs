using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Parameters;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Repos
{
    public class StudentRepo<TDbContext> : RepositoryBase<TDbContext, Student>, IStudentRepo
        where TDbContext : DbContext
    {
        public StudentRepo(IDbContextFactory<TDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
