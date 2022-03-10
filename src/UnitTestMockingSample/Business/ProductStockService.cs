using System;

namespace UnitTestMockingSample.Business
{
    public class ProductStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger _logger;

        public ProductStockService(IProductRepository productRepository, IStockRepository stockRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public bool ChangeStock(int productId, int stock)
        {
            Product product;

            try
            {
                product = _productRepository.GetById(productId);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());

                return false;
            }

            if (product != null)
            {
                // bla bla stock işlem business logic'leri...

                return _stockRepository.ChangeStock(product, stock);
            }
            else
            {
                _logger.Log("Stok bilgisi değiştirilemedi.");

                return false;
            }
        }
    }
}
