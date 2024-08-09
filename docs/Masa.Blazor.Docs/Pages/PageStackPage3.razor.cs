namespace Masa.Blazor.Docs.Pages;

public partial class PageStackPage3
{
    private string sourceCode =
        """
        <PStackPageBarInit Title="Page 3">
            <ActionContent>
                <MButton IconName="mdi-home"
                         OnClick="@(() => NavController.Clear())">
                </MButton>
            </ActionContent>
        </PStackPageBarInit>

        <a @onclick="@(() => NavController.GoBackToPage("/blazor/examples/page-stack/page1"))">Go back to page 1</a>
        """;
}