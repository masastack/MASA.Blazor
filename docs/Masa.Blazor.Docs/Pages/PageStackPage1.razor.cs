﻿using Masa.Blazor.Presets;

namespace Masa.Blazor.Docs.Pages;

public partial class PageStackPage1
{
    private string sourceCode =
        """
        @inject PageStackNavController NavController
        @inherits PStackPageBase

        <PPageStackLink Href="/blazor/examples/page-stack/page2/xyz">
            Navigate to Page 2
        </PPageStackLink>

        <a @onclick="@GoToPage4">
            Navigate to Page 4
        </a>
        <span class="ml-2 body-2">
            State from page 4: @_stateFromPage4
        </span>

        @code {
            private string? _stateFromPage4;
        
            private void GoToPage4() =>
                NavController.Push("/blazor/examples/page-stack/page4");
        
            protected override void OnPageActivated(object? state) =>
                _stateFromPage4 = state?.ToString();
        }
        """;

    private DateTimeOffset _pageCreatedAt = DateTimeOffset.UtcNow;
    private string? _stateFromPage4;

    private void GoToPage4()
    {
        NavController.Push("/blazor/examples/page-stack/page4");
    }

    protected override void OnPageActivated(object? state)
    {
        _stateFromPage4 = state?.ToString() ?? "___";
    }
}