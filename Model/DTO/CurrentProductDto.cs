namespace DatabaseCommerce.Model.DTO
{
    public record CurrentProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string CategoryName { get; init; }
        public decimal NetPrice { get; init; }
        public decimal GrossPrice { get; init; }
        public decimal VatRate { get; init; }
        public bool IsDiscount { get; init; }
        public decimal? NetLowest { get; init; }
        public decimal? GrossLowest { get; init; }
    }
}
