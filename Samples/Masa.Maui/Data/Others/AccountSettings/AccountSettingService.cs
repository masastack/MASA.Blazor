namespace Masa.Maui.Data.Others.AccountSettings
{
    public static class AccountSettingService
    {
        public static AccountDto GetAccount() => new AccountDto("johndoe", "John Doe", "granger007@hogward.com", "Crystal Technologies");

        public static List<CountryDto> GetCountryList() => new()
        {
            new("1", "USA"),
            new("2", "India"),
            new("3", "Canada"),
        };

        public static InformationDto GetInformation() => new("", DateOnly.FromDateTime(DateTime.Now), "1", "", 6562542568);

        public static SocialDto GetSocial() => new("https://www.twitter.com", "", "", "https://www.linkedin.com", "", "");
    }
}
