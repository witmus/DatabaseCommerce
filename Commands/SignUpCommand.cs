namespace DatabaseCommerce.Commands
{
    public record SignUpCommand(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email,
        string Password,
        string Town,
        string Street,
        int BuildingNumber,
        int? ApartmentNumber,
        string ZipCode,
        string Country)
    {
    }
}
