import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig([
  {
    input: "./src/components/configurable-table/index.ts",
    output: [
      {
        file: "../TemplateTable/src/Masa.Blazor.Components.TemplateTable/MTemplateTable.razor.js",
        format: "esm",
        sourcemap: true,
      },
    ],
    plugins: [
      typescript(),
      // terser()
    ],
  },
]);
