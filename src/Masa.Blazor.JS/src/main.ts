import * as infiniteScroll from "./components/infinite-scroll";
import * as slider from "./components/slider";
import * as textarea from "./components/textarea";
import * as interop from "./interop";
import { MarkdownParser } from "./wrappers/markdown-it";

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
    ...textarea,
    ...infiniteScroll,
  },
  xgplayerPlugins: [],
};
