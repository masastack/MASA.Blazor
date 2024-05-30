import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/monaco-editor/index.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/proxies/monaco-editor-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
