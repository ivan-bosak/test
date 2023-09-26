using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface ICalculatorProvider
{
    ICalculator GetCalculator(IncentiveType incentiveType);
}

