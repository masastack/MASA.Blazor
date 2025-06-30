import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/wrappers/swiper/video-feed.ts",
  output: [
    {
      file: "../MASA.Blazor.JSComponents.Swiper/wwwroot/video-feed.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
