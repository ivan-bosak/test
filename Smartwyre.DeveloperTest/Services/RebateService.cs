using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore rebateDataStore;
    private readonly IProductDataStore productDataStore;
    private readonly ICalculatorProvider calculatorProvider;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, ICalculatorProvider calculatorProvider)
    {
        this.rebateDataStore = rebateDataStore;
        this.productDataStore = productDataStore;
        this.calculatorProvider = calculatorProvider;
    }
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = this.rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = this.productDataStore.GetProduct(request.ProductIdentifier);

        var calculator = this.calculatorProvider.GetCalculator(rebate.Incentive);

        var calculationData = new CalculationData(rebate, product, request);
        var result = calculator.CalculateRebateResult(calculationData);

        if (result.Success)
        {
            var rebateAmount = calculator.CalculateAmount(calculationData);
            this.rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }
}
