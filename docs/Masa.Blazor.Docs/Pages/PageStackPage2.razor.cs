namespace Masa.Blazor.Docs.Pages;

public partial class PageStackPage2
{
    public string sourceCode =
        """
        @inject PageStackNavController NavController
        
        <a @onclick="UpdateId">
            Update Id
        </a>

        <a @onclick="@GoToPage3">
            Navigate to Page 3
        </a>
        
        @code {
            private void GoToPage3()
            {
                NavController.Push("/blazor/examples/page-stack/page3");
            }
            
            private void UpdateId()
            {
                var newId = Random.Shared.Next(100, 999);
                NavController.Replace($"/blazor/examples/page-stack/page2/{newId}");
            }
        }
        """;
}