import Xgplayer, { Events, IPlayerOptions } from "xgplayer";
import MusicPreset, { Music } from "xgplayer-music";
import Mobile from "xgplayer/es/plugins/mobile";
import Play from "xgplayer/es/plugins/play";
import Playbackrate from "xgplayer/es/plugins/playbackRate";
import Progress from "xgplayer/es/plugins/progress";
import Time from "xgplayer/es/plugins/time";
import Volume from "xgplayer/es/plugins/volume";
import DynamicBg from "xgplayer/es/plugins/dynamicBg";

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

    if (!DynamicBg.isSupport) {
      console.warn(
        "[Xgplayer] DynamicBg plugin is not supported in this environment."
      );
    }
  }

  invokeVoid(prop: string, ...args: any[]) {
    if (this.player[prop] && typeof this.player[prop] === "function") {
      this.player[prop](...args);
    }
  }

  setProp(prop: string, value: any) {
    if (this.player[prop] !== undefined) {
      this.player[prop] = value;
    }
  }

  togglePlay(force: boolean = null) {
    console.log("togglePlay", force, this.player.paused);
    let toPlay = force !== null ? force : this.player.paused;
    console.log("toPlay", toPlay);

    if (toPlay) {
      this.player.play();
    } else {
      this.player.pause();
    }
  }

  toggleMuted(force: boolean = null) {
    console.log("toggleMuted", force, this.player.muted);
    let toMuted = force !== null ? force : !this.player.muted;
    console.log("toMuted", toMuted);
    this.player.muted = toMuted;
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

  getMetadata(video: HTMLVideoElement) {
    // 获取视频的宽高
    const videoWidth = video.videoWidth || 0;
    const videoHeight = video.videoHeight || 0;

    // 计算宽高比
    const aspectRatio = videoHeight > 0 ? videoWidth / videoHeight : 0;

    return {
      width: videoWidth,
      height: videoHeight,
      aspectRatio: aspectRatio,
    };
  }

  init(url: IPlayerOptions["url"], options: XgplayerOptions) {
    let playerOptions: IPlayerOptions = {
      el: this.el,
      url,
    };

    if (options.fullscreenTarget) {
      const fullscreenEl = document.querySelector(options.fullscreenTarget);
      if (fullscreenEl) {
        options.fullscreenTarget = fullscreenEl;
      }
    }

    if (options.cssFullscreen && options.cssFullscreen.target) {
      const el = document.querySelector(options.cssFullscreen.target);
      if (el) {
        options.cssFullscreen.target = el;
      }
    }

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

    this.debug("options", playerOptions);

    this.player = new Xgplayer(playerOptions);

    this.player.on(Events.FULLSCREEN_CHANGE, val => {
      this.debug("Fullscreen change", val);
      this.handle.invokeMethodAsync("OnFullscreenChange", val);
    });
    this.player.on(Events.CSS_FULLSCREEN_CHANGE, val => {
      this.debug("CSS Fullscreen change", val);
      this.handle.invokeMethodAsync("OnCssFullscreenChange", val);
    });
    this.player.on(Events.LOADED_DATA, val => {
      const metadata = this.getMetadata(val.player.media);
      this.debug("Loaded data", metadata);
      this.handle.invokeMethodAsync("OnMetadataLoaded", metadata);
    });
    this.player.on(Events.VIDEO_RESIZE, val => {
      this.debug("Video resize", val);
      this.handle.invokeMethodAsync("OnResize", val);
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

  private debug(message: string, ...args: any[]) {
    if (window.MasaBlazor.debug.includes("xgplayer")) {
      console.debug(`[Xgplayer] ${message}: `, ...args);
    }
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
