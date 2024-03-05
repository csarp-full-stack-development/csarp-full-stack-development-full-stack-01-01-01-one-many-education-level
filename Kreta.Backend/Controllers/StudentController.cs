using Kreta.Backend.Repos;
using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models.SchoolCitizens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController<Student, StudentDto>
    {
        private readonly IStudentRepo _repo;
        private readonly StudentAssambler _assambler;
        public StudentController(StudentAssambler assembler, IStudentRepo repo) : base(assembler, repo)
        {
            _repo = repo;
            _assambler = assembler;
        }

        [HttpGet("included")]
        public async Task<IActionResult> SelectAllIncludedAsync()
        {
            List<Student>? students = new();
            if (_repo != null)
            {
                try
                {
                    students = await _repo.SelectAllIncluded().ToListAsync();
                    return Ok(students.Select(entity => _assambler.ToDto(entity)));
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }
    }
}
