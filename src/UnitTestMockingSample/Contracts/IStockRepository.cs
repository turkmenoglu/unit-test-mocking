namespace UnitTestMockingSample
{
    public interface IStockRepository
    {
        bool ChangeStock(Product product, int stock);
    }
}