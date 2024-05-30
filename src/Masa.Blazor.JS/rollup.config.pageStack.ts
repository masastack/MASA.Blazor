import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/components/page-stack/index.ts",
  output: [
    {
      file: "../Masa.Blazor/Presets/PageStack/PPageStack.razor.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
