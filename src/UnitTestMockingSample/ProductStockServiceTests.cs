using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnitTestMockingSample.Business;

namespace UnitTestMockingSample
{
    [TestClass]
    public class ProductStockServiceTests
    {
        private IProductRepository _productRepository;
        private IStockRepository _stockRepository;
        private ILogger _logger;

        private ProductStockService _productStockService;

        [TestInitialize]
        public void Initialize()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _stockRepository = Substitute.For<IStockRepository>();
            _logger = Substitute.For<ILogger>();

            _productStockService = new ProductStockService(_productRepository, _stockRepository, _logger);
        }

        [TestMethod]
        public void ChangeStock_WhenProductNotNull_Change()
        {
            // Arrange
            _productRepository.GetById(Arg.Is<int>(a => a < 10))
                .Returns(new Product() { Name = "Asus PDA", Stock = 100});

            _stockRepository.ChangeStock(Arg.Any<Product>(), Arg.Any<int>())
                .Returns(true);

            // Act
            var result = _productStockService.ChangeStock(5, 50);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ChangeStock_WhenProductNull_WriteLogMessage()
        {
            // Act
            _productStockService.ChangeStock(5, 50);

            // Assert
            _logger.Received().Log(Arg.Any<string>());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _productRepository = null;
            _stockRepository = null;
            _logger = null;
        }
    }
}