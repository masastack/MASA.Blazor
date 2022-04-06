const defaultUploadConfig = {
    action:  '',
    methods: 'POST', 
    token: '',
    tokenName:'',
    name: 'file',
    accept: 'image/png, image/gif, image/jpeg, image/bmp, image/x-icon',
    pathKey:'path'
}
const defaultToolbarContainer = [
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
]

export function init(quillElement, obj, toolBarContainer, readOnly,
    placeholder, theme, isMarkdown,uploadConfig) {
    if (!Quill || !quillElement) return;
    let toolbar = {
        container: toolBarContainer.childNodes.length == 0 ? defaultToolbarContainer : toolBarContainer,
        handlers: {}
    };
    toolbar.handlers.image = function image() {
        var self = this;
        handlerImage(self, uploadConfig);
    };
    var options = {
        modules: {
            toolbar: toolbar,
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
            obj.invokeMethodAsync('HandleOnBlurAsync');
        } else {
            obj.invokeMethodAsync('HandleOnFocusAsync');
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

function handlerImage(self, uploadConfig) {
    var _uploadConfig = uploadConfig || defaultUploadConfig;
    var fileInput = self.container.querySelector('input.ql-image[type=file]')
    if (fileInput === null) {
        fileInput = document.createElement('input')
        fileInput.setAttribute('type', 'file')
        // 设置图片参数名
        if (_uploadConfig.name) {
            fileInput.setAttribute('name', _uploadConfig.name)
        }
        // 可设置上传图片的格式
        fileInput.setAttribute('accept', _uploadConfig.accept)
        fileInput.setAttribute('multiple', 'multiple')
        fileInput.classList.add('ql-image')
        // 监听选择文件
        fileInput.addEventListener('change', function () {
            if (fileInput.files.length === 0) {
                return;
            }
            if (uploadConfig) {
                uploadFilePic(_uploadConfig, self.quill, fileInput, 0);
            }
            else {
                for (var i = 0; i < fileInput.files.length; i++) {
                    insertLogo(self.quill);
                }
                fileInput.value = '';
            }
        })

        self.container.appendChild(fileInput)
    }
    fileInput.click()
}

function uploadFilePic(uploadConfig, quill, fileInput, index) {
    // 创建formData
    var formData = new FormData()
    var files = fileInput.files;
    formData.append(uploadConfig.name, files[index])
    // 如果需要token且存在token
    if (uploadConfig.token) {
        formData.append(uploadConfig.tokenName, uploadConfig.token)
    }
    var oReq = new XMLHttpRequest();
    oReq.onreadystatechange = function () {
        if (oReq.readyState == 4 && oReq.status == 200) {
            var json = JSON.parse(oReq.responseText);
            var pathKey = uploadConfig.pathKey || "path";
            var url = json[pathKey];
            var length = quill.getSelection().index;
            quill.insertEmbed(length, 'image', url);
            quill.setSelection(length + 1);
            index += 1;
            if (index < files.length) {
                uploadFilePic(uploadConfig, quill, fileInput, index);
            }
            else {
                fileInput.value = '';
            }
        }
    }
    oReq.open(uploadConfig.methods, uploadConfig.action);
    oReq.send(formData);
}
function insertLogo(quill) {
    var length = quill.getSelection().index
    quill.insertEmbed(length, 'image', "https://cdn.masastack.com/stack/images/website/masa-blazor/logo.png")
    quill.setSelection(length + 1)
}