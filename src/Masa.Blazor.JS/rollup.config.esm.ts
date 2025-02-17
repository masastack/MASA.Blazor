import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: {
    "components/input/index": "./src/components/input/index.ts",
    "components/overlay/scroll-strategy":
      "./src/components/overlay/scroll-strategy.ts",
    "components/page-stack/touch": "./src/components/page-stack/touch.ts",
    "components/page-stack/index": "./src/components/page-stack/index.ts",
    "components/navigation-drawer/touch":
      "./src/components/navigation-drawer/touch.ts",
    "components/scroll-to-target/index":
      "./src/components/scroll-to-target/index.ts",
    "components/transition/index": "./src/components/transition/index.ts",

    "mixins/activatable/index": "./src/mixins/activatable.ts",
    "mixins/intersect/index": "./src/mixins/intersect.ts",
    "mixins/outside-click/index": "./src/mixins/outside-click.ts",
    "mixins/resize/index": "./src/mixins/resize.ts",

    // the following files are introduced in the main.ts
    // "components/slider/index": "./src/components/slider/index.ts",
    // "components/textarea/index": "./src/components/textarea/index.ts",
    // "components/infinite-scroll/index": "./src/components/infinite-scroll/index.ts",
  },
  output: [
    {
      dir: "../MASA.Blazor/wwwroot/js",
      format: "es",
      chunkFileNames: "chunks/[name].js",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
