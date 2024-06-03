namespace DatabaseCommerce.Model.DTO
{
    public record InvoiceDataDto
    {
        public DateTime OrderDate { get; set; }
        public UserWithAddressDto ReceiverWithAddress { get; set; } = default!;
        public string InvoiceNumber { get; set; } = default!;
        public IEnumerable<OrderProductDto> OrderPositions { get; set; } = default!;
    }
}
