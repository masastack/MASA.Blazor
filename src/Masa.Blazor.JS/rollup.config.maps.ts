import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

export default defineConfig({
  input: "./src/proxies/maps/baidumap/index.js",
  output: [
    {
      file: "../Masa.Blazor/wwwroot/js/proxies/baidumap-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [terser()],
});
