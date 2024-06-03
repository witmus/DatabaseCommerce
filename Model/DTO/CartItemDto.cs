namespace DatabaseCommerce.Model.DTO
{
    public record CartItemDto()
    {
        public int CurrentProductId { get; init; }
        public int Amount { get; init; }
        public decimal NetPrice { get; init; }
        public decimal GrossPrice { get; init; }
        public decimal NetTotal { get; init; }
        public decimal GrossTotal { get; init; }
        public string ProductName { get; init; }
        public string CategoryName { get; init; }
    }
}
