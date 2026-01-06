import { defineConfig } from "rollup";

import terser from "@rollup/plugin-terser";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/drawflow/index.ts",
  output: [
    {
      file: "../MASA.Blazor/wwwroot/js/wrappers/drawflow-proxy.js",
      format: "esm",
    },
  ],
  plugins: [
    typescript(),
    terser(),
  ],
});
