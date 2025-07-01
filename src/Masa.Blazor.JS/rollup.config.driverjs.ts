import { defineConfig } from "rollup";
import css from "rollup-plugin-import-css";

import resolve from "@rollup/plugin-node-resolve";
import terser from "@rollup/plugin-terser";
import typescript from "@rollup/plugin-typescript";
export default defineConfig({
  input: "./src/wrappers/driverjs/index.ts",
  output: [
    {
      file: "../MASA.Blazor.JSComponents.DriverJS/wwwroot/MDriverJS.js",
      format: "esm",
    },
  ],
  plugins: [
    typescript(),
    resolve(),
    terser(),
    css({
      output: "driver.css",
    }),
  ],
});
