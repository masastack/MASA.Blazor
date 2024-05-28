import Xgplayer, { Events, IPlayerOptions } from "xgplayer";
import MusicPreset, { Music } from "xgplayer-music";
import Mobile from "xgplayer/es/plugins/mobile";
import Play from "xgplayer/es/plugins/play";
import Playbackrate from "xgplayer/es/plugins/playbackRate";
import Progress from "xgplayer/es/plugins/progress";
import Time from "xgplayer/es/plugins/time";
import Volume from "xgplayer/es/plugins/volume";

export type XgplayerOptions = Omit<
  IPlayerOptions,
  "id" | "el" | "url" | "plugins"
> & {
  music?: Music;
};

class XgplayerProxy {
  el: HTMLElement;
  initOptions: XgplayerOptions;
  player: Xgplayer;
  handle: DotNet.DotNetObject;

  constructor(
    selector: string,
    url: IPlayerOptions["url"],
    options: XgplayerOptions,
    handle: DotNet.DotNetObject
  ) {
    const el: HTMLElement = document.querySelector(selector);

    if (!el) {
      throw new Error(
        "[Xgplayer] this selector of DOM node that player to mount on is required."
      );
    }

    if (!url) {
      throw new Error("[Xgplayer] this media resource url is required.");
    }

    this.initOptions = options;
    this.el = el;
    this.handle = handle;
    this.init(url, options);
  }

  invokeVoid(prop: string, ...args: any[]) {
    if (this.player[prop] && typeof this.player[prop] === "function") {
      this.player[prop](...args);
    }
  }

  getPropsAndStates() {
    return {
      autoplay: this.player.autoplay,
      crossOrigin: this.player.crossOrigin,
      currentSrc: this.player.currentSrc,
      currentTime: this.player.currentTime,
      duration: this.player.duration,
      cumulateTime: this.player.cumulateTime,
      volume: this.player.volume,
      muted: this.player.muted,
      defaultMuted: this.player.defaultMuted,
      playbackRate: this.player.playbackRate,
      loop: this.player.loop,
      src: this.player.src,
      lang: this.player.lang,
      version: this.player.version,
      state: this.player.state,
      ended: this.player.ended,
      paused: this.player.paused,
      networkState: this.player.networkState,
      readyState: this.player.readyState,
      isFullscreen: this.player.isFullscreen,
      isCssFullscreen: this.player.isCssfullScreen,
      isSeeking: this.player.isSeeking,
      isActive: this.player.isActive,
    };
  }

  init(url: IPlayerOptions["url"], options: XgplayerOptions) {
    let playerOptions: IPlayerOptions = {
      el: this.el,
      url,
    };

    if (options.music) {
      playerOptions = {
        ...playerOptions,
        mediaType: "audio",
        presets: [MusicPreset],
        plugins: [Mobile, Progress, Play, Playbackrate, Time, Volume],
        ...options,
      };
    } else {
      playerOptions = {
        ...playerOptions,
        ...options,
      };
    }

    if (window.MasaBlazor.xgplayerPlugins) {
      playerOptions.plugins = [
        ...(playerOptions.plugins ?? []),
        ...window.MasaBlazor.xgplayerPlugins,
      ];

      playerOptions = {
        ...playerOptions,
        ...window.MasaBlazor.xgplayerPluginOptions,
      };
    }

    this.player = new Xgplayer(playerOptions);

    this.player.on(Events.FULLSCREEN_CHANGE, (val) => {
      this.handle.invokeMethodAsync("OnFullscreenChange", val);
    });
    this.player.on(Events.CSS_FULLSCREEN_CHANGE, (val) => {
      this.handle.invokeMethodAsync("OnCssFullscreenChange", val);
    });

    if (!options.music) {
      const fullScreen = this.el.querySelector(".xgplayer-fullscreen");
      if (fullScreen) {
        fullScreen.addEventListener("touchend", this.onFullscreenTouchend);
      }
    }
  }

  onFullscreenTouchend = () => {
    this.handle.invokeMethodAsync("OnFullscreenTouchend");
  };

  destroy() {
    this.el &&
      this.el.removeEventListener("touchend", this.onFullscreenTouchend);
    this.player.destroy();
    this.player = null;
    this.handle.dispose();
  }
}

export function init(
  selector: string,
  url: IPlayerOptions["url"],
  options: XgplayerOptions,
  handle: DotNet.DotNetObject
) {
  const instance = new XgplayerProxy(selector, url, options, handle);
  return instance;
}
