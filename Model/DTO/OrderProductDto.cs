namespace DatabaseCommerce.Model.DTO
{
    public record OrderProductDto
    {
        public string Name { get; init; } = default!;
        public int Amount { get; init; } = default!;
        public decimal NetPrice { get; init; } = default!;
        public decimal GrossPrice { get; init; } = default!;
        public decimal VatRate { get; init; } = default!;
        public decimal NetTotal { get; init; } = default!;
        public decimal VatTotal { get; init; } = default!;
        public decimal GrossTotal { get; init; } = default!;
    }
}
