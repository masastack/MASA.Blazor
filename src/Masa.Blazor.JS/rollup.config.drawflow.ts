import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/drawflow/index.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/proxies/drawflow-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
