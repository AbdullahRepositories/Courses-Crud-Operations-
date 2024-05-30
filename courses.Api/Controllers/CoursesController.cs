using AutoMapper;
using courses.Api.Entities;
using courses.Api.Models;
using courses.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses.Api.Controllers
{
    
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        //[HttpGet("{courseId}", Name = "GetCoursesForAuthor")]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                 return NotFound();
            }
            var coursesForAutor=_courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAutor));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId,CourseForCreationDto comingCourse) { 
          if(!_courseLibraryRepository.AuthorExists(authorId)) 
            { 
                return NotFound();
            }
            var courseEntity = _mapper.Map<Course>(comingCourse);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();
            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute(nameof(GetCourseForAuthor), new { authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);
        }
    }
}
