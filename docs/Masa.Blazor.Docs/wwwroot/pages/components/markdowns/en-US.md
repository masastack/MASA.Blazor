---
title: Markdown
desc: "Packaging based on [vditor](https://github.com/Vanessa219/vditor)"
tag: JS Proxy
---

## Usage

Markdown Examples

<masa-example file="Examples.components.markdowns.Usage"></masa-example>

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

<masa-example file="Examples.components.markdowns.Counter"></masa-example>

#### Width and height

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set height

<masa-example file="Examples.components.markdowns.HeightAndWidth"></masa-example>

#### Mode

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set mode,Support SV, IR, WYSIWYG

<masa-example file="Examples.components.markdowns.Mode"></masa-example>

#### Readonly

Whether to set to readonly mode.

<masa-example file="Examples.components.markdowns.Readonly"></masa-example>

#### Theme

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set theme

<masa-example file="Examples.components.markdowns.Theme"></masa-example>

#### Customize Toolbar

Configure [Options](https://ld246.com/article/1549638745630#options) attribute to set toolbar

<masa-example file="Examples.components.markdowns.Toolbar"></masa-example>

#### Upload

Configure [Options](https://ld246.com/article/1549638745630#options-upload) attribute to set upload parameters,this is just a demonstration of how to configure the upload parameters. Please modify your upload API address.

<masa-example file="Examples.components.markdowns.Upload"></masa-example>

### Events

#### Before all upload

```html
//Call JS to handle the upload example
let demo = {
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
                    _that.demo.uploadFile(element,  index);
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

window.demo = demo;
```

<masa-example file="Examples.components.markdowns.BeforeAllUpload"></masa-example>

#### Some events

Focus, blur, select,esc,ctrl+enter and other events. For more events, please refer to API.

<masa-example file="Examples.components.markdowns.SomeEvents"></masa-example>