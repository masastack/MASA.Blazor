function init() {
    initLanguages();

    let splitter = document.getElementById('splitter');
    if (splitter) {
        addSplitterEventListener(splitter);
    } else {
        setTimeout(() => {
            splitter = document.getElementById('splitter');
            addSplitterEventListener(splitter);
        }, 1000)
    }
}

function addSplitterEventListener(splitter) {
    var mouseDownHandler = function (e) {
        // Prevent text selection
        e.preventDefault();

        // Set initial positions
        var startPos = e.clientX;
        var startLeftWidth = code.offsetWidth;
        var startRightWidth = render.offsetWidth;

        // Define the mouse move handler
        var mouseMoveHandler = function (e) {
            // Calculate the new widths
            var newLeftWidth = startLeftWidth + (e.clientX - startPos);
            var newRightWidth = startRightWidth - (e.clientX - startPos);

            // Update the widths
            code.style.width = newLeftWidth + 'px';
            render.style.width = newRightWidth + 'px';
        };

        // Define the mouse up handler
        var mouseUpHandler = function () {
            // Remove the handlers
            document.removeEventListener('mousemove', mouseMoveHandler);
            document.removeEventListener('mouseup', mouseUpHandler);
        };

        // Add the handlers
        document.addEventListener('mousemove', mouseMoveHandler);
        document.addEventListener('mouseup', mouseUpHandler);
    };

    var code = document.getElementById('code');
    var render = document.getElementById('render');

    splitter.addEventListener('mousedown', mouseDownHandler);
}

function initLanguages() {
    function createDependencyProposals(range) {
        return [
            {
                label: 'MButton',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa button component",
                insertText: '<MButton></MButton>',
                range: range,
            },
            {
                label: 'MAlert',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MAlert component",
                insertText: '<MAlert></MAlert>',
                range: range,
            },
            {
                label: 'MResponsive',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MResponsive component",
                insertText: '<MResponsive></MResponsive>',
                range: range,
            },
            {
                label: 'MAvatar',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MAvatar component",
                insertText: '<MAvatar></MAvatar>',
                range: range,
            },
            {
                label: 'MBadge',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MBadge component",
                insertText: '<MBadge></MBadge>',
                range: range,
            },
            {
                label: 'MBanner',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MBanner component",
                insertText: '<MBanner></MBanner>',
                range: range,
            },
            {
                label: 'PBlockText ',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa PBlockText  component",
                insertText: `<PBlockText Primary="@DateOnly.FromDateTime(DateTime.Now).ToString()"\n' +
                    '            Secondary="@TimeOnly.FromDateTime(DateTime.Now).ToString()">\n' +
                    '</PBlockText>`,
                range: range,
            },
            {
                label: 'MBottomNavigation',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MBottomNavigation component",
                insertText: '<MBottomNavigation></MBottomNavigation>',
                range: range,
            },
            {
                label: 'MBottomSheet',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MBottomSheet component",
                insertText: '<MBottomSheet></MBottomSheet>',
                range: range,
            },
            {
                label: 'MAlert',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MAlert component",
                insertText: '<MAlert></MAlert>',
                range: range,
            },
            {
                label: 'MBreadcrumbs',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MBreadcrumbs component",
                insertText: '<MBreadcrumbs\n' +
                    '\tDivider="/">\n' +
                    '</MBreadcrumbs>',
                range: range,
            },
            {
                label: 'MCard',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MCard component",
                insertText: '<MCard Elevation="2"></MCard>',
                range: range,
            },
            {
                label: 'MChip',
                kind: monaco.languages.CompletionItemKind.Function,
                documentation: "masa MChip component",
                insertText: '  <MChip></MChip>',
                range: range,
            }
        ];
    }

    monaco.languages.registerCompletionItemProvider("razor", {
        provideCompletionItems: function (model, position) {
            var word = model.getWordUntilPosition(position);
            var range = {
                startLineNumber: position.lineNumber,
                endLineNumber: position.lineNumber,
                startColumn: word.startColumn,
                endColumn: word.endColumn,
            };
            return {
                suggestions: createDependencyProposals(range),
            };
        },
    });

}

function parseToDOM(htmlstring) {
    const tpl = document.createElement('template');
    tpl.innerHTML = htmlstring;
    return tpl.content;
}

function addScript(scriptNode) {
    if (document.getElementById(scriptNode.id) != null)
        return;

    var node = parseToDOM(scriptNode.content);

    if (scriptNode.nodeType == 0) {
        document.body.appendChild(node);
    } else {
        document.head.appendChild(node);
    }
}

export {
    init, addScript
}