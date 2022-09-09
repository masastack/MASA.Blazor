namespace Masa.Maui.Data.Dashboard.ECommerce;

public static class ECommerceService
{
    public static List<CompanyDto> GetCompanyList() => new List<CompanyDto>()
    {
        new("Dixons", "Technology", "23.4k", "$891.2", "68%", "/img/avatar/1.svg", "meguc@ruj.io", 1, "in 24 hours", false),
        new("Motels", "Grocery", "78k", "$668.51", "97%", "/img/avatar/2.svg", "vecav@hodzi.co.uk", 2, "in 2 days", true),
        new("Zipcar", "Fashion", "162", "$522.29", "62%", "/img/avatar/3.svg", "davcilse@is.gov", 3, "in 5 days", true),
        new("Owning", "Technology", "214", "$291.01", "88%", "/img/avatar/4.svg", "meguc@ruj.io", 1, "in 24 hours", true),
        new("Cafés", "Grocery", "208", "$783.93", "16%", "/img/avatar/7.svg", "meguc@ruj.io", 2, "in 24 hours", false),
    };

    public static int[][] GetOrderChartData() => new int[][] { new[] { 0, 0 }, new[] { 1, 2 }, new[] { 2, 1 }, new[] { 3, 3 }, new[] { 4, 2 }, new[] { 5, 4 } };

    public static int[] GetProfitChartData() => new[] { 2, 6, 4, 2, 4 };

    public static int[] GetEarningsChartData() => new[] { 53, 31, 16 };

    public static int[][] GetRevenueReportChartData() => new int[][] { new[] { 100, 180, 300, 250, 100, 50, 200, 140, 80 }, new[] { -180, -100, -70, -250, -130, -100, -90, -120 } };

    public static int[] GetBudgetChartData() => new[] { 150, 260, 160, 200, 150, 100, 200, 140 };
}