import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/components/scroll-to-target/index.ts",
  output: [
    {
      file: "../MASA.Blazor/wwwroot/js/components/scroll-to-target.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
