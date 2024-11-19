import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input:{
    "components/page-stack/touch": "./src/components/page-stack/touch.ts",
    "components/page-stack/index": "./src/components/page-stack/index.ts",
    "components/navigation-drawer/touch": "./src/components/navigation-drawer/touch.ts",
    "mixins/touch": "./src/mixins/touch.ts",
  },
  output: [
    {
      dir: "../MASA.Blazor/wwwroot/js",
      format: "es",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
