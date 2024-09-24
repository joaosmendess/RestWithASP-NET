using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETErudio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
      
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {

            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber); 
                return Ok(sum.ToString());
            }
            return BadRequest("invalid input");
        }
            [HttpGet("subtract/{firstNumber}/{secondNumber}")]
            public IActionResult GetSubtraction(string firstNumber, string secondNumber) 
            {
                 if (IsNumeric(firstNumber) && IsNumeric(secondNumber) )
                 {
                    var subtracion  = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber); 
                    return Ok(subtracion.ToString());
                 }
                 return BadRequest("invalid input");
            }
       [HttpGet("division/{firstNumber}/{secondNumber}")]
            public IActionResult GetDivision(string firstNumber, string secondNumber) 
            {
                 if (IsNumeric(firstNumber) && IsNumeric(secondNumber) )
                 {
                    var subtracion  = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber); 
                    return Ok(subtracion.ToString());
                 }
                 return BadRequest("invalid input");
            }


         [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
            public IActionResult GetMultiplication(string firstNumber, string secondNumber) 

            {
                 if (IsNumeric(firstNumber) && IsNumeric(secondNumber) )
                 {
                    var multiplication  = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber); 
                    return Ok(multiplication.ToString());
                 }
                 return BadRequest("invalid input");
            }

            [HttpGet("media/{firstNumber}/{secondNumber}")]

              public IActionResult GetMedia(string firstNumber, string secondNumber)
              {
                 if (IsNumeric(firstNumber) && IsNumeric(secondNumber) )
                 {
                    var media  = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2; 
                    return Ok(media.ToString());
                 }
                 return BadRequest("invalid input");
              }

          [HttpGet("square-root/{firstNumber}")]

              public IActionResult GetSquareRoot(string firstNumber)
              {
                 if (IsNumeric(firstNumber)  )
                 {
                    var squareRoot  = Math.Sqrt((double)ConvertToDecimal(firstNumber)) ; 
                    return Ok(squareRoot.ToString());
                 }
                 return BadRequest("invalid input");
              }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(strNumber,
             System.Globalization.NumberStyles.Any,
              System.Globalization.NumberFormatInfo.InvariantInfo,
               out number);
            return isNumber;
        }
    }
}
