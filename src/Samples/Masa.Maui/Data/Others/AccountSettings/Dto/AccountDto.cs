namespace Masa.Maui.Data.Others.AccountSettings.Dto
{
    public class AccountDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public AccountDto(string userName, string name, string email, string company)
        {
            UserName = userName;
            Name = name;
            Email = email;
            Company = company;
        }
    }
}
