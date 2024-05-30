import * as slider from "./components/slider";
import * as interop from "./interop";
import { MarkdownParser } from "./proxies/markdown-it";

declare global {
  interface Window {
    MasaBlazor: {
      interop: {};
      extendMarkdownIt?: (parser: MarkdownParser) => void;
      xgplayerPlugins: any[];
      xgplayerPluginOptions?: { [prop: string]: any };
    };
  }
}

window.MasaBlazor = {
  interop: {
    ...interop,
    ...slider,
  },
  xgplayerPlugins: [],
};
