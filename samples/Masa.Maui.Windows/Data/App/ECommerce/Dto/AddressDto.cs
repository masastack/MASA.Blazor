namespace Masa.Maui.Data.App.ECommerce.Dto;

public class AddressDto
{
    [Required]
    public string FullName { get; set; } = default!;

    [Required]
    public string MobileNumber { get; set; } = default!;

    [Required]
    public string HouseNo { get; set; } = default!;

    [Required]
    public string Landmark { get; set; } = default!;

    [Required]
    public string City { get; set; } = default!;

    [Required]
    public string Pincode { get; set; } = default!;

    [Required]
    public string State { get; set; } = default!;

    [Required]
    public string AddressType { get; set; } = default!;
}

