using Asp.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.PersonModel;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
      
        private readonly ILogger<PersonController> _logger;
        private  IPersonBusiness _personBusiness;
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
         public IActionResult Get()
        {

            
            return Ok(_personBusiness.FindAll());
            

        }
         
        [HttpGet("{id}")]
      public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
            {
             return NotFound();   
            }
            return Ok(person);
            
        }

      [HttpPost]
      public IActionResult Post([FromBody] PersonModel person)
        {
           if (person == null) return BadRequest();

            return Ok(_personBusiness.Create(person));
            
        }
          [HttpPut("id")]
      public IActionResult Put([FromBody] PersonModel person)
        {
           if (person == null) return BadRequest();

            return Ok(_personBusiness.Update(person));
            
        }

           
      

         [HttpDelete("{id}")]
      public IActionResult Delete(long id)
        {
          _personBusiness.Delete(id);
            return NoContent();
        }
    }
}