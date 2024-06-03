namespace DatabaseCommerce.Model.DTO
{
    public record UserWithAddressDto
    {
        public string ReceiverName { get; init; } = default!;
        public string Town { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string ZipCode { get; init; } = default!;
        public int BuildingNumber { get; init; }
        public int? ApartmentNumber { get; init; }
    }
}
