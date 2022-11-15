# Contributing to MASA Blazor

## Fork and pull request

See [Contributing to projects](https://docs.github.com/en/get-started/quickstart/contributing-to-projects).

## Local development 

### Setting up your environment

Required software:
- .NET 7 is required.

### Development

1. Check out #776, choose and tell us the task you want to help.
2. Open **src/Masa.Blazor.NewDocs.sln**.
3. Set **Masa.Docs.Server** as the Startup project. 
4. Use and run the **http** profile. 

### wwwroot in Masa.Docs.Shared

- **data/nav.json**: the metadata for generating navigation.
  - If each item is an `object`, the title value in the object is the **key** of **i18n**.
  - If each item is a `string`, then the string value is the **key** of **i18n**.
  - The **key** needs to correspond to the directory name under **docs/pages/components**.
- **data/page-to-api.json**: the metadata for generating the SELECT for multiple APIs.
  - Default rule: the directory name under **docs/pages/components** will be converted to the Camel-Case, removing the `s` at the end (if `s` exists), and adding the `M` character at the beginning. For example, **alerts** would be convert to **MAlert**.
  - Others: for those names that do not apply by default rule, you need to explicitly write them in **data/page-to-api.json**. For example, `grids` will be converted to `MGrid` by default, but the API of `MGrid` does not exist, so you need to explicitly write the mapping rules. 
- **data/apis/[culture].json**: the metadata for generating the description for specific API. Refer to [vuetify's](https://github.com/vuetifyjs/vuetify/tree/v2.6.12/packages/api-generator/src/locale/en).
- **docs/pages**: the markdown file for generating page.
- **docs/pages/components/[component]/[culture].md**:
  - The front matter for generating the title of page and the related pages at the end of page.
  - So the h1(#) is unnecessary.
  - Custom elements are registered in `CircuitRootComponentOptionsExtensions.cs`, such as `<[component]-usage></[component]-usage>`.
  - The file value of `<masa-example></masa-example>` is the combination of the corresponding razor namespace and file name in the **/Examples** directory.
- **locale**: the metadata for i18n.
