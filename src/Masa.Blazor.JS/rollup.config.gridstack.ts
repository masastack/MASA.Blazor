import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/gridstack/index.ts",
  output: [
    {
      file: "../Masa.Blazor.JSComponents.Gridstack/wwwroot/gridstack-interop.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), commonjs(), terser()],
});
