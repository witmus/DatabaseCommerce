namespace DatabaseCommerce.Commands
{
    public record CreateCategoryCommand(
        string Name,
        decimal VatRate)
    {
    }
}
