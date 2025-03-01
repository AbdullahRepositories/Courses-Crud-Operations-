using courses.Api.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace courses.Api.Models
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "Title must be different form description")]

    public abstract class CourseForManipulationDto 
    {
        [Required]
        public string Title { get; set; }

        public virtual string Description { get; set; }
    }
}
