import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/components/overlay/scrollStrategies.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/scrollStrategies.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
