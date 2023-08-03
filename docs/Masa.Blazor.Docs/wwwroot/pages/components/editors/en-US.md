---
title: Editor
desc: "Rich text editor based on [Quill](https://quilljs.com/) encapsulation"
tag: "JS Proxy"
---

## Usage

If you want to upload pictures, use the `Upload` parameter to set the image server address.This parameter is not set in all examples of this component, but is replaced by the default image.

<masa-example file="Examples.components.editors.Usage"></masa-example>

## CSS file

```html
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill.snow.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill.bubble.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quill-emoji.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/quill/1.3.6/quilljs-markdown-common-style.css" rel="stylesheet">
```

## JS file

```html
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quill.js"></script>
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quilljs-markdown.js"></script>
<script src="https://cdn.masastack.com/npm/quill/1.3.6/quill-emoji.js"></script>
```

## Examples

### Props

#### Height

`ElementStyle` property set editor height

<masa-example file="Examples.components.editors.Height"></masa-example>

#### Markdown

markdown

<masa-example file="Examples.components.editors.Markdown"></masa-example>

#### Module

Modules allow **Quill��s** behavior and functionality to be customized. For example, use a third-party module **quill-blot-formatter** can achieve the function of resizing images.


<app-alert type="warning" content="This document already references the **quill-blot-formatter.min.js** package and registers the **blotFormatter** module with **Quill** after **Blazor** startup, so it can be used directly. Please refer to the source code for details."></app-alert>

#### Custom prompt message

The prompt message when a null value is set by setting the `Placeholder` attribute.

<masa-example file="Examples.components.editors.Placeholder"></masa-example>

#### Readonly

Whether to set the editor instance to read-only mode.

<masa-example file="Examples.components.editors.ReadOnly"></masa-example>

#### Theme

Use the name of the topic. This built-in option is `bubble` or `Snow`. Note that the theme's style sheet needs to be included manually.

<masa-example file="Examples.components.editors.Theme"></masa-example>

#### Toolbar

Customize **ToolbarContent** through toolbarcontent slots.

<masa-example file="Examples.components.editors.Toolbar"></masa-example>

#### Upload picture

This is just a demonstration of how to configure the  `Upload`  parameters. Please modify your upload API address.

<masa-example file="Examples.components.editors.UploadPicture"></masa-example>

### Events

#### Before all upload

Hook before file upload, you can handle the upload logic yourself.

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

### Misc

#### Method

Some method examples.

<masa-example file="Examples.components.editors.Method"></masa-example>

