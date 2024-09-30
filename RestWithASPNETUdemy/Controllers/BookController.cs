using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Hypermedia.Filters;
using RestWithASPNETErudio.Model.BookModel;

namespace RestWithASPNETErudio.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(204)] // No content
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(401)] // Unauthorized
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(404)] // Not found
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(401)] // Unauthorized
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(201, Type = typeof(BookVO))]
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(401)] // Unauthorized
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            var createdBook = _bookBusiness.Create(book);
            return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(401)] // Unauthorized
        [ProducesResponseType(404)] // Not found
        public IActionResult Put(long id, [FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            var updatedBook = _bookBusiness.Update(book);
            if (updatedBook == null) return NotFound();

            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No content
        [ProducesResponseType(400)] // Bad request
        [ProducesResponseType(401)] // Unauthorized
        [ProducesResponseType(404)] // Not found
        public IActionResult Delete(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();

            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}