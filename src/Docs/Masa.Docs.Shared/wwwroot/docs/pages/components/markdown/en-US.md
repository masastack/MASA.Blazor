---
title: Markdown
desc: "Packaging based on [vditor](https://github.com/Vanessa219/vditor)"
tag: js-proxy
---

## Usage

Markdown Examples

<markdown-usage></markdown-usage>

## CSS file

```html
<link rel="stylesheet" href="https://cdn.masastack.com/npm/vditor/3.8.12/dist/index.css" />
```

## JS file

```html
<script src="https://cdn.masastack.com/npm/vditor/3.8.12/dist/index.min.js"></script>
```

## Examples

### Props

#### Counter

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set counter

<example file="" />

#### Width and height

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set height

<example file="" />

#### Mode

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set mode,Support SV, IR, WYSIWYG

<example file="" />

#### Readonly

Whether to set to readonly mode.

<example file="" />

#### Theme

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set theme

<example file="" />

#### Customize Toolbar

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set toolbar

<example file="" />

#### Upload

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set upload parameters,this is just a demonstration of how to configure the upload parameters. Please modify your upload API address.

<example file="" />

### Events

#### Before all upload

```html
//Call JS to handle the upload example
window.Demo.Vditor = {
    uploadFile: (element, index) => {
        let _that = this;
        let vditor = element.Vditor;
        let fileInput = element.querySelector('input[type=file]')
        //Here is the upload logic. This is just an example. You can write your own processing logic
        let formData = new FormData()
        let files = fileInput.files;
        formData.append('file', files[index])
        let oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState == 4 && oReq.status == 200) {
                let json = JSON.parse(oReq.responseText);
                let imageUrl = json.Path;
                let succFileText = "";
                if (vditor && vditor.vditor.currentMode === "wysiwyg") {
                    succFileText += `\n <img alt=${imageUrl} src="${imageUrl}">`;
                } else {
                    succFileText += `\n![${imageUrl}](${imageUrl})`;
                }
                //Insert the uploaded picture into the editor
                document.execCommand("insertHTML", false, succFileText);
                index += 1;
                if (index < files.length) {
                    _that.Demo.Vditor.uploadFile(element,  index);
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

#### Some events

Focus, blur, select,esc,ctrl+enter and other events. For more events, please refer to API.

<example file="" />