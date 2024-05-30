import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/main.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/masa-blazor.js",
      format: "iife",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), terser()],
});
