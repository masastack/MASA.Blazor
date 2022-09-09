namespace Masa.Maui.Data.Dashboard.Analytics;

public static class AnalyticsService
{
    public static int[] GetSubscribersChartData() => new[] { 28, 40, 36, 52, 38, 60, 55 };

    public static int[] GetOrdersChartData() => new[] { 10, 15, 8, 15, 7, 12, 8 };

    public static int[] GetSessionsChartData() => new[] { 75, 125, 225, 175, 125, 75, 25 };

    public static int[][] GetSalesChartData() => new int[][] { new[] { 70, 50, 90, 30, 70, 30 }, new[] { 50, 70, 30, 50, 50, 50 } };
}

