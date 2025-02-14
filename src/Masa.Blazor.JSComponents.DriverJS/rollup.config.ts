import { defineConfig } from "rollup";
import css from "rollup-plugin-import-css";

import resolve from "@rollup/plugin-node-resolve";
import terser from "@rollup/plugin-terser";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./js-module/main.ts",
  output: [
    {
      file: "MDriverJS.razor.js",
      format: "esm",
    },
  ],
  plugins: [
    typescript(),
    resolve(),
    // terser(),
    css({
      output: "wwwroot/driver.css",
    }),
  ],
});
