using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public class EducationLevelService : BaseService<EducationLevel, EducationLevelDto>, IEducationLevelService
    {
        public EducationLevelService(IHttpClientFactory? httpClientFactory, EducationLevelAssambler assambler) : base(httpClientFactory, assambler)
        {
        }
    }
}
