using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orion.Domain;

namespace Orion.Controllers
{
    /// <summary>
    /// Math endpoint - accepts a string expression and returns the calculated decimal result
    /// NOTES:
    /// - accepts a string with numbers and currently supported operators (+-*/)
    /// - does not support parentheses, but operator precedence is taken into account (*/ are applied before +-)
    /// - unary + and - operators are supported
    /// - requirements stated to support up to 5 values.  this supports more, but an interface could constrain that.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet]
        public decimal Get(string expression)
        {
            return MyMath.Calculate(expression);
        }
    }
}
