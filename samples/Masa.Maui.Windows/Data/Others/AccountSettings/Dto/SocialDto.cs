namespace Masa.Maui.Data.Others.AccountSettings.Dto
{
    public class SocialDto
    {
        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public string Google { get; set; }

        public string LinkedIn { get; set; }

        public string Instagram { get; set; }

        public string Quora { get; set; }

        public SocialDto(string twitter, string facebook, string google, string linkedIn, string instagram, string quora)
        {
            Twitter = twitter;
            Facebook = facebook;
            Google = google;
            LinkedIn = linkedIn;
            Instagram = instagram;
            Quora = quora;
        }
    }
}
