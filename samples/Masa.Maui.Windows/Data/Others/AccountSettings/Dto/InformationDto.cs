namespace Masa.Maui.Data.Others.AccountSettings.Dto
{
    public class InformationDto
    {
        public string Bio { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public long Phone { get; set; } 

        public InformationDto(string bio, DateOnly birthDate, string country, string website, long phone)
        {
            Bio = bio;
            BirthDate = birthDate;
            Country = country;
            Website = website;
            Phone = phone;
        }
    }
}
