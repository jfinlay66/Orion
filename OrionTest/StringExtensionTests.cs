using Orion.Domain;
using Orion.Exceptions;
using Orion.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrionTest
{
    public class StringExtensionTests
    {
        [Fact]
        public void TestTokenizeExpression_Valid_Success()
        {
            // arrange
            var expression = "1+ 2*3.3/4- 5.5";
            var expected = new List<Token>() 
                                { 
                                    new Token("1"),
                                    new Token('+'),
                                    new Token("2"),
                                    new Token('*'),
                                    new Token("3.3"),
                                    new Token('/'),
                                    new Token("4"),
                                    new Token('-'),
                                    new Token("5.5")
                                };

            // act
            var result = expression.TokenizeExpression();

            // assert
            Assert.Equal<Token>(expected, result);
        }

        [Fact]
        public void TestTokenizeExpression_Unary_Success()
        {
            // arrange
            var expression = "+1+ 2*3.3/4- -5.5";
            var expected = new List<Token>()
                                {
                                    new Token("+1"),
                                    new Token('+'),
                                    new Token("2"),
                                    new Token('*'),
                                    new Token("3.3"),
                                    new Token('/'),
                                    new Token("4"),
                                    new Token('-'),
                                    new Token("-5.5")
                                };

            // act
            var result = expression.TokenizeExpression();

            // assert
            Assert.Equal<Token>(expected, result);
        }

        [Fact]
        public void TestTokenizeExpression_AdjacentOperators_Failure()
        {
            // arrange
            var expression = "1+ 2*/3.3/4- -5.5";

            // act

            // assert
            Assert.Throws<MyMathException>(() => expression.TokenizeExpression());
        }

        [Fact]
        public void TestTokenizeExpression_InvalidCharacters_Failure()
        {
            // arrange
            var formula = "ABC1239";

            // act

            // assert
            Assert.Throws<MyMathException>(() => formula.TokenizeExpression());
        }

        [Fact]
        public void TestTokenizeExpression_Empty_Failure()
        {
            // arrange
            var formula = "";

            // act

            // assert
            Assert.Throws<MyMathException>(() => formula.TokenizeExpression());
        }

        [Fact]
        public void TestTokenizeExpression_StartTokenIsOperator_Failure()
        {
            // arrange
            var formula = "*1+2";

            // act

            // assert
            Assert.Throws<MyMathException>(() => formula.TokenizeExpression());
        }

        [Fact]
        public void TestTokenizeExpression_EndTokenIsOperator_Failure()
        {
            // arrange
            var formula = "1+2*";

            // act

            // assert
            Assert.Throws<MyMathException>(() => formula.TokenizeExpression());
        }
    }
}
