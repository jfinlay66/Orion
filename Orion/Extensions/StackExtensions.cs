using Orion.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Extensions
{
    /// <summary>
    /// All extensions for Stacks
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Computes the result of all values and operators on the stack
        /// Operators should all be at precedence 1, otherwise, they will just be applied left to right
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static decimal ComputeAll(this Stack<Token> stack)
        {
            var result = stack.Pop().Value;

            while (stack.Count > 0)
            {
                var op = stack.Pop().Operator;
                var nextValue = stack.Pop().Value;
                result = op.Compute(nextValue, result);
            }

            return result;
        }
    }
}
