using Orion.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Domain
{
    /// <summary>
    /// Operator class
    /// </summary>
    public class Operator : IEquatable<Operator>
    {
        protected readonly char _operator;
        private static readonly char[] _operators = new char[] { '+', '-', '*', '/' };
        private static readonly char[] _level1 = { '+', '-' };

        /// <summary>
        /// Precedence of this operator - this needs to be tweaked as more operators are added
        /// </summary>
        public int Precedence { get { return _level1.Contains(_operator) ? 1 : 2; } }

        /// <summary>
        /// Determine if a character represents an operator
        /// </summary>
        /// <param name="character">character to check</param>
        /// <returns>True if operator, false if not</returns>
        public static bool IsOperator(char character)
        {
            return _operators.Contains(character);
        }

        /// <summary>
        /// Determine if a character represents one of the unary operators
        /// </summary>
        /// <param name="character">character to check</param>
        /// <returns>True if unary operator, false if not</returns>
        public static bool IsUnaryOperator(char character)
        {
            return _level1.Contains(character);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operatorChar">Operator character</param>
        public Operator(char operatorChar)
        {
            _operator = operatorChar;
        }

        /// <summary>
        /// Apply operator to two values
        /// </summary>
        /// <param name="value1">Decimal value 1</param>
        /// <param name="value2">Decimal value 2</param>
        /// <returns>Resulting decimal value</returns>
        public decimal Compute(decimal value1, decimal value2)
        {
            var result = _operator switch
            {
                '+' => value1 + value2,
                '-' => value1 - value2,
                '*' => value1 * value2,
                '/' => value1 / value2,
                // this should never occur because of previous validations, but including for safety
                _ => throw new MyMathException($"Invalid operator found: {_operator}")
            };

            return result;
        }

        /// <summary>
        /// Equals comparison
        /// </summary>
        /// <param name="other">Other instance of Operator</param>
        /// <returns>True if equals, False if not</returns>
        public bool Equals(Operator other)
        {
            if (other == null)
                return false;

            if (this._operator == other._operator && this.Precedence == other.Precedence)
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

            Operator tokenObj = obj as Operator;
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
            int hashcode = this._operator.GetHashCode();
            hashcode ^= this.Precedence;

            return hashcode;
        }

        /// <summary>
        /// == operator on 2 instances
        /// </summary>
        /// <param name="operator1">1st operator instance</param>
        /// <param name="operator2">1nd operator instance</param>
        /// <returns>True if equals, False if not</returns>
        public static bool operator ==(Operator operator1, Operator operator2)
        {
            if (((object)operator1) == null || ((object)operator2) == null)
                return Object.Equals(operator1, operator2);

            return operator1.Equals(operator2);
        }

        /// <summary>
        /// != operator on 2 instances
        /// </summary>
        /// <param name="operator1">1st operator instance</param>
        /// <param name="operator2">1nd operator instance</param>
        /// <returns>True if not equals, False if equals</returns>
        public static bool operator !=(Operator operator1, Operator token2)
        {
            if (((object)operator1) == null || ((object)token2) == null)
                return !Object.Equals(operator1, token2);

            return !(operator1.Equals(token2));
        }
    }
}
