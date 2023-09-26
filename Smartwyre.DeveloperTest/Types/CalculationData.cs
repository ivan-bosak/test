using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types;

public class CalculationData
{
    public CalculationData(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        Rebate = rebate;
        Product = product;
        Request = request;
    }

    public Rebate Rebate { get; }
    public Product Product { get; }
    public CalculateRebateRequest Request { get; }
}
