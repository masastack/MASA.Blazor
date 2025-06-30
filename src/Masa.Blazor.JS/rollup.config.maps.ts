import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/maps/baidumap/index.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/wrappers/baidumap.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
