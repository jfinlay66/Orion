using Orion.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Domain
{
    /// <summary>
    /// Token class
    /// </summary>
    public class Token : IEquatable<Token>
    {
        /// <summary>
        /// Does this token represent an operator?
        /// </summary>
        public bool IsOperator { get; private set; }

        /// <summary>
        /// Operator instance
        /// </summary>
        public Operator Operator { get; private set; }

        /// <summary>
        /// Value of the token, if not an operator
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Decimal value of the token</param>
        public Token(decimal value)
        {
            IsOperator = false;
            Value = value;
        }

        /// <summary>
        /// Operator char of the token
        /// </summary>
        /// <param name="operatorChar"></param>
        public Token(char operatorChar)
        {
            IsOperator = true;
            Operator = new Operator(operatorChar);
        }

        /// <summary>
        /// String representation of the token
        /// </summary>
        /// <param name="value">string value - should contain a decimal</param>
        public Token(string value)
        {
            IsOperator = false;

            decimal result;

            var success = decimal.TryParse(value, out result);

            if (!success)
            {
                throw new MyMathException($"Unable to parse decimal value: {value}");
            }

            Value = result;
        }

        /// <summary>
        /// Equals comparison
        /// </summary>
        /// <param name="other">Other instance of Token</param>
        /// <returns>True if equals, False if not</returns>
        public bool Equals(Token other)
        {
            if (other == null)
                return false;

            if (this.IsOperator && this.Operator == other.Operator)
                return true;
            else if (!this.IsOperator && this.Value == other.Value)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Equals comparison with object
        /// </summary>
        /// <param name="obj">Object to compare with</param>
        /// <returns>True if equals, False if not</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Token tokenObj = obj as Token;
            if (tokenObj == null)
                return false;
            else
                return Equals(tokenObj);
        }

        /// <summary>
        /// Get instance hash code
        /// </summary>
        /// <returns>Hashcode of instance values</returns>
        public override int GetHashCode()
        {
            if (this.IsOperator)
                return this.Operator.GetHashCode();
            else
                return this.Value.GetHashCode();
        }

        /// <summary>
        /// == operator on 2 instances
        /// </summary>
        /// <param name="operator1">1st token instance</param>
        /// <param name="operator2">1nd token instance</param>
        /// <returns>True if equals, False if not</returns>
        public static bool operator ==(Token token1, Token token2)
        {
            if (((object)token1) == null || ((object)token2) == null)
                return Object.Equals(token1, token2);

            return token1.Equals(token2);
        }

        /// <summary>
        /// != operator on 2 instances
        /// </summary>
        /// <param name="operator1">1st token instance</param>
        /// <param name="operator2">1nd token instance</param>
        /// <returns>True if not equals, False if equals</returns>
        public static bool operator !=(Token token1, Token token2)
        {
            if (((object)token1) == null || ((object)token2) == null)
                return !Object.Equals(token1, token2);

            return !(token1.Equals(token2));
        }
    }
}
