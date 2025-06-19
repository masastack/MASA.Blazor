import { defineConfig } from "rollup";

import { getBabelOutputPlugin } from "@rollup/plugin-babel";
import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import terser from "@rollup/plugin-terser";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: [
    "./js-module/mobile-viewer.ts",
    "./js-module/pdf.worker.js",
  ],
  output: [
    {
      dir: "./wwwroot/",
      entryFileNames: "[name].js",
      format: "esm",
      // sourcemap: true,
    },
  ],
  plugins: [
    typescript(),
    getBabelOutputPlugin({
      presets: ["@babel/preset-env"],
    }),
    resolve(),
    commonjs(),
    terser(),
  ],
  watch: {
    exclude: "node_modules/**",
  },
});