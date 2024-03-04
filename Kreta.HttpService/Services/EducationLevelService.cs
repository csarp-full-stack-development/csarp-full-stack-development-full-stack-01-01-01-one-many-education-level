using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Kreta.HttpService.Services
{
    public class EducationLevelService : BaseService<EducationLevel, EducationLevelDto>, IEducationLevelService
    {
        private readonly StudentAssambler _studentAssambler;
        public EducationLevelService(IHttpClientFactory? httpClientFactory, EducationLevelAssambler assambler, StudentAssambler studentAssambler) : base(httpClientFactory, assambler)
        {
            _studentAssambler = studentAssambler;
        }

        public async Task<List<EducationLevel>> SelectAllIncludedAsync()
        {
            if (_httpClient is not null)
            {
                try
                {
                    List<EducationLevelDto>? resultDto = await _httpClient.GetFromJsonAsync<List<EducationLevelDto>>($"api/EducationLevel/included");
                    if (resultDto is not null)
                    {
                        List<EducationLevel> result = resultDto.Select(entity => _assambler.ToModel(entity)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<EducationLevel>();
        }

        public async Task<List<Student>> GetStudentsBy(Guid educationId)
        {
            if (_httpClient is not null)
            {
                try
                {
                    List<StudentDto>? resultDto = await _httpClient.GetFromJsonAsync<List<StudentDto>>($"api/EducationLevel/students/{educationId}");
                    if (resultDto is not null)
                    {
                        List<Student> result = resultDto.Select(student => _studentAssambler.ToModel(student)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<Student>();
        }
    }
}
