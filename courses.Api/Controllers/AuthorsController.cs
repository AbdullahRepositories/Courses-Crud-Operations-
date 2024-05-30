using courses.Api.Helpers;
using courses.Api.Services;
using courses.Api.Models;
using courses.Api.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using courses.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using static Azure.Core.HttpHeader;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
        }
        // [HttpGet(Name = "GetWeatherForecast")]

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters? authorsResourceParameters)
        {
            var authorsFromReop = _courseLibraryRepository.GetAuthors(authorsResourceParameters);


            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromReop));

        }



       
        [HttpGet("{authorId}", Name = nameof(GetAuthor))]
        public ActionResult GetAuthor(Guid authorId)
        {
            var authorFromRep = _courseLibraryRepository.GetAuthor(authorId);


            if (authorFromRep is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(authorFromRep));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
           var authorEntity= _mapper.Map<Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();

            var authorToReturn=_mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute(nameof(GetAuthor), 
                new { authorId = authorToReturn.Id },
                authorToReturn);

        }


        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}