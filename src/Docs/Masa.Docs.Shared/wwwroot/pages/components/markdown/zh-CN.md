---
title: Markdown（Markdown编辑器）
desc: "基于[vditor](https://github.com/Vanessa219/vditor)封装"
tag: js-proxy
---

## 使用

Markdown编辑器示例

<markdown-usage></markdown-usage>

## css文件

```html
<link rel="stylesheet" href="https://cdn.masastack.com/npm/vditor/3.8.12/dist/index.css" />
```

## js文件

```html
<script src="https://cdn.masastack.com/npm/vditor/3.8.12/dist/index.min.js"></script>
```

## 示例

### 属性

#### 计数器

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置计数器

<example file="" />

#### 自定义宽高

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置高度

<example file="" />

#### 模式

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置模式,支持sv、 ir、wysiwyg

<example file="" />

#### 只读

是否设置为只读模式。

<example file="" />

#### 主题

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置主题

<example file="" />

#### 自定义工具栏

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置工具栏

<example file="" />

#### 上传

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置上传参数，这里只是演示如何配置upload参数，请修改你的上传api地址。

<example file="" />

### 事件

#### 文件上传前事件

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

#### 一些事件

focus、blur、select、esc、ctrl+enter等事件，更多事件请参考api。

<example file="" />