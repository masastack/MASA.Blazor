namespace Masa.Maui.Data.Others.AccountSettings.Dto
{
    public class CountryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CountryDto(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
