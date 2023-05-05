---
title: Editor（富文本编辑器）
desc: "基于[Quill](https://quilljs.com/)封装的富文本编辑器"
tag: "JS代理"
---

## 使用

如果要上传图片，请使用`Upload`参数，来设置图片服务器地址。本组件的所有示例均未设置此参数，而是用默认图片来代替。

<masa-example file="Examples.components.editors.Usage"></masa-example>

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

<masa-example file="Examples.components.editors.Height"></masa-example>

#### Markdown

支持markdown解析。

<masa-example file="Examples.components.editors.Markdown"></masa-example>

#### 模块

模块允许定制 **Quill** 的行为和功能。例如使用第三方模块 **quill-blot-formatter** 可以实现调整图片大小的功能。


<app-alert type="warning" content="此文档已经引用了 **quill-blot-formatter.min.js**包，并且在**Blazor**启动后给**Quill**注册了**blotFormatter**模块，因此可以直接使用。具体可参考源码。"></app-alert>

#### 自定义提示信息

通过设置 `Placeholder` 属性来设置空值时的提示消息。

<masa-example file="Examples.components.editors.Placeholder"></masa-example>

#### 只读

是否将编辑器实例设置为只读模式。

<masa-example file="Examples.components.editors.ReadOnly"></masa-example>

#### 主题

使用主题的名称。这个内置的选项是 `bubble` 或 `snow` 。注意，主题的样式表需要手动包含。

<masa-example file="Examples.components.editors.Theme"></masa-example>

#### 自定义工具栏

通过 **ToolbarContent** 插槽自定义工具栏。

<masa-example file="Examples.components.editors.Toolbar"></masa-example>

#### 上传图片

这里只是演示如何配置 `Upload` 参数，请修改你的上传api地址。

<masa-example file="Examples.components.editors.UploadPicture"></masa-example>

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

<masa-example file="Examples.components.editors.BeforeAllUpload"></masa-example>

### 其他

#### 方法

一些方法示例。

<masa-example file="Examples.components.editors.Method"></masa-example>