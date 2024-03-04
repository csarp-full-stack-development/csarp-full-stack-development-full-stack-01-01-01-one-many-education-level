using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;

namespace Kreta.HttpService.Services
{
    public interface IEducationLevelService : IBaseService<EducationLevel>
    {
        public Task<List<Student>> GetStudentsBy(Guid educationId);
    }
}
