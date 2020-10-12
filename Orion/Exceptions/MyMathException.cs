using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Exceptions
{
    /// <summary>
    /// Custom exception class
    /// </summary>
    public class MyMathException : Exception
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        public MyMathException()
        {

        }

        /// <summary>
        /// Constructor to take message only
        /// </summary>
        /// <param name="message">Message to hand back to caller</param>
        public MyMathException(string message) : base(message)
        {

        }

        /// <summary>
        /// Pattern for wrapping inner exception (so we don't lose stack trace, etc.)
        /// </summary>
        /// <param name="message">Our message</param>
        /// <param name="ex">Original exception that we're wrapping</param>
        public MyMathException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
