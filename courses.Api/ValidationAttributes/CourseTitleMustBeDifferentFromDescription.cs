using System.ComponentModel.DataAnnotations;
using courses.Api.Models;
namespace courses.Api.ValidationAttributes
{
    public class CourseTitleMustBeDifferentFromDescription: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
    {
        var course = (CourseForManipulationDto)validationContext.ObjectInstance;

        if (course.Title == course.Description)
        {
            return new ValidationResult(ErrorMessage,
                new[] { nameof(CourseForManipulationDto) });
        }

        return ValidationResult.Success;
    }
}

}
