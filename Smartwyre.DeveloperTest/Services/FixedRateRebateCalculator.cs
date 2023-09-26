using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedRateRebateCalculator : ICalculator
{
    public IncentiveType IncentiveType => IncentiveType.FixedRateRebate;

    public CalculateRebateResult CalculateRebateResult(CalculationData calculationData)
    {
        var rebate = calculationData.Rebate;
        var product = calculationData.Product;
        var request = calculationData.Request;
        var result = new CalculateRebateResult
        {
            Success = rebate is not null && product is not null
                        && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) && rebate.Amount != 0 
                        && product.Price != 0 && request.Volume != 0
        };

        return result;
    }

    public decimal CalculateAmount(CalculationData calculationData)
        => calculationData.Rebate.Amount * calculationData.Request.Volume;
}
