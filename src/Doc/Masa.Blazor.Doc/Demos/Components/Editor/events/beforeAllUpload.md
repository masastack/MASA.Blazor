---
order: 1
title:
  zh-CN: 文件上传前事件
  en-US: Before all upload
---

## zh-CN

文件上传之前的钩子，你可以自己处理上传逻辑。

```html
uploadFilePic: (quillElement, element) => {
    var quill = quillElement.__quill;
    var fileInput = element.querySelector('input.ql-image[type=file]')
    var files = fileInput.files;
}
```

## en-US

The hook before file upload, you can handle the upload logic yourself.

```html
uploadFilePic: (quillElement, element) => {
    var quill = quillElement.__quill;
    var fileInput = element.querySelector('input.ql-image[type=file]')
    var files = fileInput.files;
}
```
