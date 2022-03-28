const defaultToolbar = {
    container: [
        ['bold', 'italic', 'underline', 'strike'],
        ['blockquote', 'code-block'],
        [{ 'header': 1 }, { 'header': 2 }],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }],
        [{ 'indent': '-1' }, { 'indent': '+1' }],
        [{ 'direction': 'rtl' }],
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        [{ 'color': [] }, { 'background': [] }],
        [{ 'font': [] }],
        [{ 'align': [] }],
        ['clean'],
        ['emoji'],
        ['link', 'image', 'video']
    ],
    handlers: {
        'emoji': function () { }
    }
};


export function init(quillElement,obj, toolBar, readOnly,
    placeholder, theme, isMarkdown) {
    if (!Quill || !quillElement) return;
    var options = {
        modules: {
            toolbar: toolBar.childNodes.length == 0 ? defaultToolbar : toolBar,
            "emoji-toolbar": true,
            "emoji-shortname": true,
        },
        placeholder: placeholder,
        readOnly: readOnly,
        theme: theme
    };
    const editor = new Quill(quillElement, options);
    if (isMarkdown) {
        const markdownOptions = {};
        const quillMarkdown = new QuillMarkdown(editor, markdownOptions);
    }
    obj.invokeMethodAsync("HandleRenderedAsync");
    editor.on('selection-change', range => {
        if (!range) {
            obj.invokeMethodAsync('HandleOnBlurAsync', editor);
        } else {
            obj.invokeMethodAsync('HandleOnFocusAsync', editor);
        }
    })
    editor.on('text-change', (delta, oldDelta, source) => {
        const text = editor.getText();
        const html = editor.root.innerHTML;
        obj.invokeMethodAsync('HandleInputAsync', html);
    })
}
export function getContent(quillElement) {
    return JSON.stringify(quillElement.__quill.getContents());
}
export function getText(quillElement) {
    return quillElement.__quill.getText();
}
export function getHtml(quillElement) {
    return quillElement.__quill.root.innerHTML;
}
export function setContent(quillElement, quillContent) {
    content = JSON.parse(quillContent);
    return quillElement.__quill.setContents(content, 'api');
}
export function setHtml(quillElement, quillHTMLContent) {
    return quillElement.__quill.root.innerHTML = quillHTMLContent;
}
export function enableEditor(quillElement, mode) {
    quillElement.__quill.enable(mode);
}
export function insertImage(quillElement, imageURL) {
    var Delta = Quill.import('delta');
    editorIndex = 0;
    if (quillElement.__quill.getSelection() !== null) {
        editorIndex = quillElement.__quill.getSelection().index;
    }
    return quillElement.__quill.updateContents(
        new Delta()
            .retain(editorIndex)
            .insert({ image: imageURL },
                { alt: imageURL }));
}
export default {
    init,
    getContent,
    getText,
    getHtml,
    setHtml,
    enableEditor,
    insertImage
}