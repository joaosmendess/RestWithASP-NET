using Asp.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Hypermedia.Filters;

namespace RestWithASPNETErudio.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            var persons = _personBusiness.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [ProducesResponseType((201), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();

            var createdPerson = _personBusiness.Create(person);
            return CreatedAtAction(nameof(Get), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(long id, [FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();

            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null) return NotFound();
            
            return Ok(updatedPerson);
        }

        [HttpPatch("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id, [FromBody] JsonPatchDocument<PersonVO> patch)
        {
            if (patch == null) return BadRequest();

            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();

            patch.ApplyTo(person);
            var updatedPerson = _personBusiness.Update(person);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();

            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}