---
title: Editor
desc: "Rich text editor based on [Quill](https://quilljs.com/) encapsulation"
tag: js-proxy
---

## Usage

Examples

<editor-usage></editor-usage>

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

<example file="" />

#### Markdown

markdown

<example file="" />

#### Custom prompt message

The prompt message when a null value is set by setting the `Placeholder` attribute.

<example file="" />

#### Readonly

Whether to set the editor instance to read-only mode.

<example file="" />

#### Theme

Use the name of the topic. This built-in option is `bubble` or `Snow`. Note that the theme's style sheet needs to be included manually.

<example file="" />

#### Toolbar

Customize **ToolbarContent** through toolbarcontent slots.

<example file="" />

#### Upload picture

This is just a demonstration of how to configure the  `Upload`  parameters. Please modify your upload API address.

<example file="" />

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

<example file="" />

### Misc

#### Method

Some method examples.

<example file="" />

