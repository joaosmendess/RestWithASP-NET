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
