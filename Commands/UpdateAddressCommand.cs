namespace DatabaseCommerce.Commands
{
    public record UpdateAddressCommand(
        int UserId,
        string Town,
        string Street,
        int BuildingNumber,
        int? ApartmentNumber,
        string ZipCode,
        string Country)
    {
    }
}
