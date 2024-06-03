namespace DatabaseCommerce.Commands
{
    public record ChangeCartItemAmountCommand(
        int UserId,
        int ProductId,
        int Amount)
    {
    }
}
