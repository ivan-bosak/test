using FluentAssertions;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> rebateDataStoreMock = new();
        private readonly Mock<IProductDataStore> productDataStoreMock = new();
        private readonly Mock<ICalculatorProvider> calculatorProviderMock = new();
        private readonly Mock<ICalculator> calculatorMock = new();

        [Theory, InlineData(1, 2)]
        public void Calculate_WithValidRequest_CallsDependenciesAndStoresResult(int rebateId, int productId)
        {
            // Arrange
            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, calculatorProviderMock.Object);

            var request = CreateRequest(rebateId, productId);

            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate };
            var product = new Product();

            SetupMocks(rebateId, productId, rebate, product);

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            result.Success.Should().BeTrue();
            rebateDataStoreMock.Verify(ds => ds.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Once);
        }

        [Theory, InlineData(1, 2)]
        public void Calculate_WithInvalidRequest_ReturnsFailure(int rebateId, int productId)
        {
            // Arrange
            var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, calculatorProviderMock.Object);

            var request = CreateRequest(rebateId, productId);

            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate };

            SetupMocks(rebateId, productId, rebate, isSucceed: false);

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            result.Success.Should().BeFalse();
            rebateDataStoreMock.Verify(ds => ds.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
        }


        private static CalculateRebateRequest CreateRequest(int rebateId, int productId)
            => new()
            {
                RebateIdentifier = rebateId.ToString(),
                ProductIdentifier = productId.ToString(),
            };

        private void SetupMocks(int rebateId, int productId, Rebate rebate = null, Product product = null, bool isSucceed = true)
        {
            rebateDataStoreMock.Setup(ds => ds.GetRebate(rebateId.ToString())).Returns(rebate);
            productDataStoreMock.Setup(ds => ds.GetProduct(productId.ToString())).Returns(product);

            calculatorMock.Setup(cl => cl.CalculateRebateResult(It.IsAny<CalculationData>()))
                .Returns(new CalculateRebateResult { Success = isSucceed });
            calculatorProviderMock.Setup(cp => cp.GetCalculator(IncentiveType.FixedRateRebate))
                .Returns(calculatorMock.Object);
        }
    }
}
