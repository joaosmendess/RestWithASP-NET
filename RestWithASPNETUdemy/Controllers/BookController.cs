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

          private  IBookBusiness _bookBusiness;
          public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }
  [HttpGet]
  [TypeFilter(typeof(HyperMediaFilter))]
         public IActionResult Get()
        {

            
            return Ok(_bookBusiness.FindAll());
            

        }
         
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
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
      public IActionResult Post([FromBody] BookVO book)
        {
           if (book == null) return BadRequest();

            return Ok(_bookBusiness.Create(book));
            
        }
          [HttpPut("id")]
          [TypeFilter(typeof(HyperMediaFilter))]
      public IActionResult Put([FromBody] BookVO book)
        {
           if (book == null) return BadRequest();

            return Ok(_bookBusiness.Update(book));
            
        }

           
      

         [HttpDelete("{id}")]
      public IActionResult Delete(long id)
        {
          _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}