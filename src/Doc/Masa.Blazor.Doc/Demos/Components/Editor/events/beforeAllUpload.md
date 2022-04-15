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

## en-US

The hook before file upload, you can handle the upload logic yourself.

```html
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
