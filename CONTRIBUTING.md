# Contributing Guide

## Getting started

If you are not clear about the contributing process, please refer to [Contributing to projects](https://docs.github.com/en/get-started/quickstart/contributing-to-projects).

## How can I get involved?

We have an [`status/help wanted`](https://github.com/masastack/MASA.Blazor/issues?q=is%3Aissue+is%3Aopen+label%3A%22status%2Fhelp+wanted%22) label on our issue tracker to indicate tasks which contributors can pick up.

If you've found something you'd like to contribute to, leave a comment in the issue so everyone is aware.

## Local development 

### Required software

- .NET 7 is required.

### Clone code

```shell
git clone --recursive https://github.com/masastack/Masa.Blazor.git
cd Masa.Blazor
git submodule foreach git checkout main
```

### Choose an appropriate solution 

- **Masa.Blazor.sln**: the solution for all projects.
- **Masa.Blazor.Playground.sln**: the solution for developing the MASA Blazor library with a small project for testing.
- **Masa.Blazor.Docs.sln**: the solution for developing the docs site.

### Project structure

#### Masa.Blazor.Docs.ApiGenerator

A source generator for generating the API metadata of MASA Blazor.

#### wwwroot in Masa.Docs.Shared

- **data/nav.json**: the metadata for generating navigation.
  - If each item is an `object`, the title value in the object is the **key** of **i18n**.
  - If each item is a `string`, then the string value is the **key** of **i18n**.
  - The **key** needs to correspond to the directory name under **docs/pages/components**.
- **data/page-to-api.json**: the metadata for generating the SELECT for multiple APIs.
  - Default rule: the directory name under **docs/pages/components** will be converted to the Camel-Case, removing the `s` at the end (if `s` exists), and adding the `M` character at the beginning. For example, **alerts** would be convert to **MAlert**.
  - Others: for those names that do not apply by default rule, you need to explicitly write them in **data/page-to-api.json**. For example, `grids` will be converted to `MGrid` by default, but the API of `MGrid` does not exist, so you need to explicitly write the mapping rules. 
- **data/apis/[culture].json**: the metadata for generating the description for specific API. Refer to [vuetify's](https://github.com/vuetifyjs/vuetify/tree/v2.6.12/packages/api-generator/src/locale/en).
- **pages**: the markdown file for generating page.
- **pages/components/[component]/[culture].md**:
  - The front matter for generating the title of page and the related pages at the end of page.
  - So the h1(#) is unnecessary.
  - Custom elements are registered in `CircuitRootComponentOptionsExtensions.cs`, such as `<[component]-usage></[component]-usage>`.
  - The file value of `<masa-example></masa-example>` is the combination of the corresponding razor namespace and file name in the **/Examples** directory.
- **locale**: the metadata for i18n.
