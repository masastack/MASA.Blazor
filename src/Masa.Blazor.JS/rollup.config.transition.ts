import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/transition/index.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/transition.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
