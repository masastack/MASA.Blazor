---
title: Editor（富文本编辑器）
desc: "基于[Quill](https://quilljs.com/)封装的富文本编辑器"
tag: js-proxy
---

## 使用

富文本编辑器示例

<editor-usage></editor-usage>

## css文件

```html
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill.snow.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill.bubble.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill-emoji.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quilljs-markdown-common-style.css" rel="stylesheet">
```

## js文件

```html
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quill.js"></script>
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quilljs-markdown.js"></script>
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quill-emoji.js"></script>
```

## 示例

### 属性

#### 自定义高度

通过 `ElementStyle` 属性设置高度

<example file="" />

#### Markdown

支持markdown解析。

<example file="" />

#### 自定义提示信息

通过设置 `Placeholder` 属性来设置空值时的提示消息。

<example file="" />

#### 只读

是否将编辑器实例设置为只读模式。

<example file="" />

#### 主题

使用主题的名称。这个内置的选项是 `bubble` 或 `snow` 。注意，主题的样式表需要手动包含。

<example file="" />

#### 自定义工具栏

通过 **ToolbarContent** 插槽自定义工具栏。

<example file="" />

#### 上传图片

这里只是演示如何配置 `Upload` 参数，请修改你的上传api地址。

<example file="" />

### 事件

#### 文件上传前事件

文件上传之前的钩子，你可以自己处理上传逻辑。

```javascript
//Call JS to handle the upload example
window.Demo.Quill = {
    uploadFilePic: (quillElement, element, index) => {
        let _that = this;
        let quill = quillElement.__quill;//get quill editor
        let fileInput = element.querySelector('input.ql-image[type=file]')//get fileInput
        //Here is the upload logic. This is just an example. You can write your own processing logic
        let formData = new FormData()
        let files = fileInput.files;
        formData.append('file', files[index])
        let oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState == 4 && oReq.status == 200) {
                let json = JSON.parse(oReq.responseText);
                let url = json.Path;
                let length = quill.getSelection().index;
                quill.insertEmbed(length, 'image', url);//Insert the uploaded picture into the editor
                quill.setSelection(length + 1);
                index += 1;
                if (index < files.length) {
                    _that.Demo.Quill.uploadFilePic(quillElement, element, index);
                }
                else {
                    fileInput.value = '';
                }
            }
        }
        oReq.open('POST', "/api/upload");//Please change your upload API address
        oReq.send(formData);
    }
};
```

<example file="" />

### 其他

#### 方法

一些方法示例。

<example file="" />