using CourseRegistrationAPIDemo.Dtos;
using Microsoft.AspNetCore.Mvc;
using CourseRegistrationAPIDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseRegistrationAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseRegistrationsController : ControllerBase
    {
        private readonly ICourseRegistrationService _courseRegistrationService;
        public CourseRegistrationsController(ICourseRegistrationService courseRegistrationService)
        {
            _courseRegistrationService = courseRegistrationService;
        }


        // GET: api/<CourseRegistrationsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CourseRegistrationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CourseRegistrationsController>
        [HttpPost]
        public IActionResult Post([FromBody] CourseRegistrationCreateDto courseRegistration)
        {
          var result= _courseRegistrationService.RegisterStudent(courseRegistration);
            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Registration Failed"
                }); 
            }

               return Created("",new
                { 
                    Message = "Registration Success",
                    Data=result
                });
            }
           

        

        // PUT api/<CourseRegistrationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CourseRegistrationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
