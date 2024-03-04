using Kreta.Backend.Repos;
using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Extensions;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationLevelController : BaseController<EducationLevel, EducationLevelDto>
    {
        private readonly IEducationLevelRepo _repo;
        private readonly EducationLevelAssambler _assambler;
        public EducationLevelController(EducationLevelAssambler assembler, IEducationLevelRepo repo) : base(assembler, repo)
        {
            _repo = repo;
            _assambler = assembler;
        }

        [HttpGet("included")]
        public async Task<IActionResult> SelectAllIncludedAsync()
        {
            List<EducationLevel>? entities = new();

            if (_repo != null)
            {
                try
                {
                    entities = await _repo.SelectAllIncluded().ToListAsync();
                    return Ok(entities.Select(entity => _assambler.ToDto(entity)));
                }
                catch (Exception ex)
                {
                }
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpGet("students/{educationLevelId}")]
        public IActionResult GetStudentsBy(Guid educationLevelId)
        {
            List<Student> students= new();
            if (_repo is not null)
            {
                students = _repo.SelectStudentsBy(educationLevelId).ToList();
                if (students== null)
                    students=new();
                return Ok(students.Select(student => student.ToDto()));
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }
    }
}
