namespace Masa.Maui.Data.Dashboard.ECommerce.Dto;

public class CompanyDto
{
    public string CompanyName { get; set; }

    public string Category { get; set; }

    public string Views { get; set; }

    public string Revenue { get; set; }

    public string Sales { get; set; }

    public string CompanyIcon { get; set; }

    public string CompanyEmail { get; set; }

    public int CategoryIcon { get; set; }

    public string ViewTime { get; set; }

    public bool Rise { get; set; }

    public CompanyDto(string companyName, string category, string views, string revenue, string sales, string companyIcon, string companyEmail, int categoryIcon, string viewTime, bool rise)
    {
        CompanyName = companyName;
        Category = category;
        Views = views;
        Revenue = revenue;
        Sales = sales;
        CompanyIcon = companyIcon;
        CompanyEmail = companyEmail;
        CategoryIcon = categoryIcon;
        ViewTime = viewTime;
        Rise = rise;
    }
}
