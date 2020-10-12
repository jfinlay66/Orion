using Orion.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrionTest
{
    public class MyMathTests
    {
        [Fact]
        public void TestCalculate_Valid_Success()
        {
            // arrange
            var formula = "1+2*3/4-5";
            decimal? expected = -2.5M;

            // act
            var result = MyMath.Calculate(formula);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
