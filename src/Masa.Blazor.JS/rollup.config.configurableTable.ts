import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig([
  {
    input: "./src/components/configurable-table/index.ts",
    output: [
      {
        file: "../Masa.Blazor.MasaTable/MTemplateTable.razor.js",
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
