namespace DatabaseCommerce.Commands
{
    public record AddToCartCommand(
        int UserId,
        int ProductId,
        int Amount)
    {
    }
}
