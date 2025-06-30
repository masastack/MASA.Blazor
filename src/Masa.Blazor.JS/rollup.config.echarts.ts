import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/echarts/index.ts",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/wrappers/echarts.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  onwarn: (warning, warn) => {
    if (warning.code === "EVAL") {
      return;
    }

    warn(warning);
  },
});
