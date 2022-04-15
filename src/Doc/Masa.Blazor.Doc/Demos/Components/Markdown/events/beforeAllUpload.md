---
order: 1
title:
  zh-CN: 文件上传前事件
  en-US: Before all upload
---

## zh-CN

文件上传之前的钩子，你可以自己处理上传逻辑。

```html
//Call JS to handle the upload example
window.Demo.Vditor = {
    uploadFile: (element, index) => {
        var _that = this;
        var vditor = element.Vditor;
        var fileInput = element.querySelector('input[type=file]')
        //Here is the upload logic. This is just an example. You can write your own processing logic
        var formData = new FormData()
        var files = fileInput.files;
        formData.append('file', files[index])
        var oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState == 4 && oReq.status == 200) {
                var json = JSON.parse(oReq.responseText);
                var imageUrl = json.Path;
                let succFileText = "";
                if (vditor && vditor.vditor.currentMode === "wysiwyg") {
                    succFileText += `\n <img alt=${imageUrl} src="${imageUrl}">`;
                } else {
                    succFileText += `\n![${imageUrl}](${imageUrl})`;
                }
                ////Insert the uploaded picture into the editor
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

## en-US

The hook before file upload, you can handle the upload logic yourself.

```html
//Call JS to handle the upload example
window.Demo.Vditor = {
    uploadFile: (element, index) => {
        var _that = this;
        var vditor = element.Vditor;
        var fileInput = element.querySelector('input[type=file]')
        //Here is the upload logic. This is just an example. You can write your own processing logic
        var formData = new FormData()
        var files = fileInput.files;
        formData.append('file', files[index])
        var oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState == 4 && oReq.status == 200) {
                var json = JSON.parse(oReq.responseText);
                var imageUrl = json.Path;
                let succFileText = "";
                if (vditor && vditor.vditor.currentMode === "wysiwyg") {
                    succFileText += `\n <img alt=${imageUrl} src="${imageUrl}">`;
                } else {
                    succFileText += `\n![${imageUrl}](${imageUrl})`;
                }
                ////Insert the uploaded picture into the editor
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
