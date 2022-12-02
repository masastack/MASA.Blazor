# Install

Let's start with MASA Blazor. MASA Blazor is the UI component library of the .NET Blazor framework for building feature-rich and fast applications.

## Prerequisites

All products of the MASA series are developed based on .Net 6.0, please make sure that you have installed <a href="https://dotnet.microsoft.com/download/dotnet/6.0" target="_blank">.NET 6.0</a> 。

## Presentation

The video content is the MASA Blazor template to create the Blazor Server demo video, you can choose to view or directly view the CLI creation part. 

<video src="https://cdn.masastack.com/stack/images/website/masa-blazor/video.mp4" controls width="100%"></video>

## CLI Creation

### Install Masa.Template

Masa.Template, contains all project templates of the MASA series. The corresponding template of MASA Blazor is named `masab` 

```shell
dotnet new --install Masa.Template
```
### Create project

Create a project based on the project template name and specify the output directory, which is the root folder of the project. 

```shell
dotnet new masab -o MasaBlazorApp
```

> The default is Server mode, through the parameter --mode WebAssembly to create a WebAssembly mode project. 

### Startup project

Switch to the MasaBlazorApp directory by command `cd MasaBlazorApp`. 
Start the project with `dotnet run`, open the corresponding URL according to the program output, and you can see the effect of the MASA Blazor template project. 

## Create manually

### Create a Blazor Server project

Create a new Blazor application named BlazorApp in the command line interface: 

```shell
dotnet new blazorserver -o BlazorApp
```

or

```shell
dotnet new blazorwasm -o BlazorApp
```

> `blazorserver` is Blazor Server App template shortname。`blazorwasm` is Blazor WebAssembly App template shortname

### Install NuGet package

```shell
dotnet add package Masa.Blazor
```

### Import resource files

####  Blazor Server

in `Pages/_Layout.cshtml` import resource files：

```html
<!--masa blazor css style-->
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<!--icon file,import need to use-->
<link href="https://cdn.jsdelivr.net/npm/@("@mdi")/font@5.x/css/materialdesignicons.min.css" rel="stylesheet">
<link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">
<!--js(should lay the end of file)-->
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```

#### Blazor WebAssembly

in `wwwroot\index.html` import resource files：

```html
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />

<link href="https://cdn.jsdelivr.net/npm/@mdi/font@5.x/css/materialdesignicons.min.css" rel="stylesheet">
<link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">

<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```
### Inject services 

Program.cs file add Masa.Blazor related services：

```csharp
// Add services to the container.
builder.Services.AddMasaBlazor();
```
### Global reference

update `_Imports.razor` file,Add:

```csharp
@using Masa.Blazor
@using BlazorComponent
```

update `Shared/MainLayout.razor` file，set MApp as root element：

```html
<MApp> //layout </MApp>
```
## Use

Refer to official documents [Component](https://masa-blazor-docs-dev.lonsid.cn/components/application),Add related components。
`dotnet run` start project Preview the MasaBlazor。

