namespace Masa.Maui.Data.App.ECommerce;

public static class BasketService
{
    public static List<BasketItemDto> GetBasketItems() => new List<BasketItemDto>
    {
        new BasketItemDto(1,"Apple Watch Series 5","Apple",4,1,"Delivery by Sun, Nov 28","12% off 3 offers Available",339.99m,"1.png",true),
        new BasketItemDto(2,"Google - Google Home - White/Slate fabric","Google",4,1,"Delivery by Wed, Dec 1","16% off 1 offers Available",129.29m,"7.png",true),
        new BasketItemDto(3,"Apple iPhone 11 (64GB, Black)","Apple",5,1,"Delivery by Thu, Nov 25","8% off 1 offers Available",669.99m,"2.png",true),
        new BasketItemDto(4,"Apple iMac 27-inch","Apple",4,1,"Delivery by Mon, Nov 29","3% off 4 offers Available",999.99m,"3.png",true),
        new BasketItemDto(5,"Apple - MacBook Air® (Latest Model) - 13.3\" Display - Silver","Apple",4,1,"Delivery by Sun, Nov 28","17% off 4 offers Available",999.99m,"5.png",false)
    };

    public static List<AddressTypeDto> GetAddressTypes() => new List<AddressTypeDto>
    {
        new AddressTypeDto("Home", "1"),
        new AddressTypeDto("Work", "2")
    };
}

