namespace Kreta.Shared.Models
{
    public class EducationLevel : IDbEntity<EducationLevel>
    {
        public Guid Id { get; set; }
        public string StudentEducationLevel { get; set; } = string.Empty;
        public int DurationOfEducation {  get; set; }

    }
}
