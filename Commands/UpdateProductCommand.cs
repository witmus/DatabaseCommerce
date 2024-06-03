namespace DatabaseCommerce.Commands
{
    public record UpdateProductCommand(
        int Id,
        string Name,
        decimal NetPrice,
        bool IsDiscount)
    {
    }
}
