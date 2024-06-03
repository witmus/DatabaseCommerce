namespace DatabaseCommerce.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal NetPrice,
        bool IsDiscount,
        string CategoryName)
    {
    }
}
