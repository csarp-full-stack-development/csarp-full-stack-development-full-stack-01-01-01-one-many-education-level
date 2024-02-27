using Kreta.Shared.Dtos;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Assamblers;

namespace Kreta.HttpService.Services
{
    public class TeacherService : BaseService<Teacher, TeacherDto>, ITeacherService
    {
        private readonly HttpClient? _httpClient;

        public TeacherService(IHttpClientFactory? httpClientFactory, TeacherAssambler assambler) : base (httpClientFactory, assambler) { }


    }
}
