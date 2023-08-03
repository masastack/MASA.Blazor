---
title: Markdown（Markdown编辑器）
desc: "基于[vditor](https://github.com/Vanessa219/vditor)封装"
tag: Js代理
---

## 使用

Markdown编辑器示例

<masa-example file="Examples.components.markdowns.Usage"></masa-example>

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

<masa-example file="Examples.components.markdowns.Counter"></masa-example>

#### 自定义宽高

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置高度

<masa-example file="Examples.components.markdowns.HeightAndWidth"></masa-example>

#### 模式

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置模式,支持sv、 ir、wysiwyg

<masa-example file="Examples.components.markdowns.Mode"></masa-example>

#### 只读

是否设置为只读模式。

<masa-example file="Examples.components.markdowns.Readonly"></masa-example>

#### 主题

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置主题

<masa-example file="Examples.components.markdowns.Theme"></masa-example>

#### 自定义工具栏

配置 [Options](https://ld246.com/article/1549638745630#options) 属性设置工具栏

<masa-example file="Examples.components.markdowns.Toolbar"></masa-example>

#### 上传

配置 [Options](https://ld246.com/article/1549638745630#options-upload) 属性设置上传参数，这里只是演示如何配置upload参数，请修改你的上传api地址。

<masa-example file="Examples.components.markdowns.Upload"></masa-example>

### 事件

#### 文件上传前事件

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

#### 一些事件

focus、blur、select、esc、ctrl+enter等事件，更多事件请参考api。

<masa-example file="Examples.components.markdowns.SomeEvents"></masa-example>