using FluentAssertions;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;


namespace Smartwyre.DeveloperTest.Tests;

public class FixedRateRebateCalculatorTests
{
    [Fact]
    public void CalculateRebateResult_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var calculator = new FixedRateRebateCalculator();
        var rebate = new Rebate { Amount = 100 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 50 };
        var request = new CalculateRebateRequest { Volume = 2 };
        var calculationData = new CalculationData(rebate, product, request);

        // Act
        var result = calculator.CalculateRebateResult(calculationData);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public void CalculateRebateResult_WithInvalidData_ReturnsFailure()
    {
        // Arrange
        var calculator = new FixedRateRebateCalculator();
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 50 };
        var request = new CalculateRebateRequest { Volume = 2 };
        var calculationData = new CalculationData(null, product, request);

        // Act
        var result = calculator.CalculateRebateResult(calculationData);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void CalculateAmount_WithValidData_ReturnsCorrectAmount()
    {
        // Arrange
        var calculator = new FixedRateRebateCalculator();
        var rebate = new Rebate { Amount = 100 };
        var request = new CalculateRebateRequest { Volume = 2 };
        var calculationData = new CalculationData(rebate, null, request);
        var expected = rebate.Amount * request.Volume;

        // Act
        var amount = calculator.CalculateAmount(calculationData);

        // Assert
        amount.Should().Be(expected);
    }
}
