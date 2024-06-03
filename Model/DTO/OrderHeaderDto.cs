namespace DatabaseCommerce.Model.DTO
{
    public record OrderHeaderDto
    {
        public string InvoiceNumber { get; init; }
        public DateTime IncomeDate { get; init; }
        public decimal GrossTotal { get; init; }
    }
}
