namespace Masa.Blazor.Docs.Pages;

public partial class PageStackPage4
{
    private string sourceCode =
        """
        @inject PageStackNavController NavController
        
        <ul>
            <li><a @onclick="@(() => NavController.Pop("AAA"))">AAA</a></li>
            <li><a @onclick="@(() => NavController.Pop("BBB"))">BBB</a></li>
            <li><a @onclick="@(() => NavController.Pop("CCC"))">CCC</a></li>
        </ul>
        """;
}