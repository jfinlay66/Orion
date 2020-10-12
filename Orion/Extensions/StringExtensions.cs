using Orion.Domain;
using Orion.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Orion.Extensions
{
    /// <summary>
    /// All string extensions
    /// </summary>
    public static class StringExtensions
    {
        private const string EXPRESSION_REGEX = @"^[0-9\.\+\-\*/ ]+$";

        /// <summary>
        /// Parse the expression string into tokens
        /// </summary>
        /// <param name="expression">string representing an expression</param>
        /// <returns>List of expression tokens</returns>
        public static List<Token> TokenizeExpression(this string expression)
        {
            // verify the expression only contains valid characters
            var match = Regex.Match(expression, EXPRESSION_REGEX);

            if (!match.Success)
            {
                throw new MyMathException($"Expression contains invalid characters: {expression}");
            }

            // step through the characters in the expression string
            var result = new List<Token>();

            var currentToken = string.Empty;

            foreach (char currentChar in expression)
            {
                // ignore blanks
                if (currentChar == ' ')
                    continue;

                // check if this character is an operator
                if (Operator.IsOperator(currentChar))
                {
                    // create token from previous decimal value (if exists) and add to list
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        result.Add(new Token(currentToken));
                        currentToken = string.Empty;
                        result.Add(new Token(currentChar));
                    }
                    // the previous character was also an operator, so this is a unary operator that should be included in the number token
                    else if (Operator.IsUnaryOperator(currentChar))
                    {
                        currentToken += currentChar;
                    }
                    // 2 invalid operators found in a row
                    else
                    {
                        throw new MyMathException($"Invalid adjacent operators located in expression: {expression}");
                    }
                }
                // not an operator, save character as part of decimal value string
                else
                {
                    currentToken += currentChar;
                }
            }

            // if still have a string token, add to list
            if (!string.IsNullOrEmpty(currentToken))
            {
                result.Add(new Token(currentToken));
            }

            // verify that the list of tokens is valid
            var msg = ValidateExpression(result);
            if (!string.IsNullOrEmpty(msg))
            {
                throw new MyMathException($"{msg}: {expression}");
            }

            return result;
        }

        /// <summary>
        /// After parsing into tokens, is the resulting list of tokens valid?
        /// </summary>
        /// <param name="tokenList">List of parsed expression tokens</param>
        /// <returns>Validation message - null if list is valid</returns>
        private static string ValidateExpression(List<Token> tokenList)
        {
            // verify there is at least 1 token
            if (tokenList.Count == 0)
                return $"Expression is empty";

            // verify first token is a value
            if (tokenList[0].IsOperator)
                return $"Expression must start with a value";

            // verify last token is a value
            if (tokenList[^1].IsOperator)
                return $"Expression must end with a value";

            return null;
        }
    }
}
