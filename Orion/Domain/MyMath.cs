using Orion.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Domain
{
    public static class MyMath
    {
        /// <summary>
        /// Determine the decimal result of calculating the string expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static decimal Calculate(string expression)
        {
            // convert string to expression tokens
            var tokenList = expression.TokenizeExpression();

            var stack = new Stack<Token>();

            // process each token
            for (int i = 0; i < tokenList.Count; i++)
            {
                var currentToken = tokenList[i];

                // if precedence is 2, immediately apply operator to next value token and previous value token (which is already on the stack)
                if (currentToken.IsOperator && currentToken.Operator.Precedence > 1)
                {
                    // get next token and pop top token, operator.execute(next, top), push result
                    var nextValue = tokenList[++i];
                    var previousValue = stack.Pop();

                    var currentResult = currentToken.Operator.Compute(previousValue.Value, nextValue.Value);
                    stack.Push(new Token(currentResult));
                }
                else
                {
                    stack.Push(currentToken);
                }
            }

            // compute remaining stack - all remaining operators are precedence 1 at this point
            var result = stack.ComputeAll();

            return result;
        }
    }
}
