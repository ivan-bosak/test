using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedCashAmountCalculator : ICalculator
{
    public IncentiveType IncentiveType => IncentiveType.FixedCashAmount;

    public CalculateRebateResult CalculateRebateResult(CalculationData calculationData)
    {
        var rebate = calculationData.Rebate;
        var product = calculationData.Product;
        var result = new CalculateRebateResult
        {
            Success = rebate is not null && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) 
                      && rebate.Amount != 0
        };

        return result;
    }

    public decimal CalculateAmount(CalculationData calculationData) => calculationData.Rebate.Amount;
}
