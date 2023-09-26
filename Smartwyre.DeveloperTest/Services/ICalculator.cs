using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface ICalculator
{
    IncentiveType IncentiveType { get; }
    decimal CalculateAmount(CalculationData calculationData);
    CalculateRebateResult CalculateRebateResult(CalculationData calculationData);
}
