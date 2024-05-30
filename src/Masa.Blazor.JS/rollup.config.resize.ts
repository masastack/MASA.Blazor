import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/mixins/resize/index.ts",
  output: [
    {
      file: "../MASA.Blazor/wwwroot/js/resize.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), terser()],
});
