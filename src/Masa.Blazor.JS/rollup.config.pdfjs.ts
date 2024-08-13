import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: [
    "./src/proxies/pdf.js/mobile-viewer.ts",
    "pdfjs-dist/legacy/build/pdf.worker.min.mjs"
  ],
  output: [
    {
      dir: "../Masa.Blazor/wwwroot/js/proxies/pdf-mobile-viewer",
      entryFileNames: "[name].js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [
    typescript(),
    resolve(),
    terser(),
  ],
  watch: {
    exclude: "node_modules/**",
  },
});
