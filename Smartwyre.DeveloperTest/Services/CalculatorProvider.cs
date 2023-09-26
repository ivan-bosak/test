using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services;

public class CalculatorProvider : ICalculatorProvider
{
    private readonly ICollection<ICalculator> calculators;

    // Assuming IoC configured and all calculators properly registered
    public CalculatorProvider(ICollection<ICalculator> calculators)
    {
        this.calculators = calculators;
    }

    public ICalculator GetCalculator(IncentiveType incentiveType)
        => calculators.Single(x => x.IncentiveType == incentiveType);
}
