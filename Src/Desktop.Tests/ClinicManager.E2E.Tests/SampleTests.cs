using MyApp;
using Xunit;

namespace ClinicManager.E2E.Tests;

public class CalculatorTests
{
    // [Fact] runs a single, parameterless test case
    [Fact]
    public void Add_TwoNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }

    // [Theory] runs the same test multiple times with different [InlineData]
    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    [InlineData(-4, true)]
    public void IsEven_VariousNumbers_ReturnsExpectedResult(int number, bool expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        bool result = calculator.IsEven(number);

        // Assert
        Assert.Equal(expected, result);
    }
}
Use code with caution.
