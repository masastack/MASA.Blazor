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
    let toPlay = force !== null ? force : this.player.paused;

    if (toPlay) {
      this.player.play();
    } else {
      this.player.pause();
    }
  }

  toggleMuted(force: boolean = null) {
    let toMuted = force !== null ? force : !this.player.muted;
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

  // 将播放器切换到音乐模式
  toMusic() {
    // 如果已经是audio模式，则不需要切换
    if (this.player.media instanceof HTMLAudioElement) {
      this.debug("Already in music mode, no need to switch.");
      return;
    }

    // 保存当前播放状态
    let currentTime = 0;
    let volume = 1;
    let playbackRate = 1;
    let muted = false;
    let paused = true;

    if (this.player) {
      currentTime = this.player.currentTime || 0;
      volume = this.player.volume || 1;
      playbackRate = this.player.playbackRate || 1;
      muted = this.player.muted || false;
      paused = this.player.paused;

      // 销毁当前播放器
      this.player.destroy();
    }

    // 创建音乐模式的选项
    const musicOptions: XgplayerOptions = {
      ...this.initOptions,
      music: this.initOptions.music || ({} as Music),
      volume: volume,
      playbackRate: playbackRate,
      muted: muted,
    };

    delete musicOptions.fullscreenTarget;
    delete musicOptions.cssFullscreen;

    // 重新初始化为音乐模式
    this.init(this.player.url, musicOptions);

    // 在播放器准备好后恢复播放状态
    this.player.once(Events.READY, () => {
      if (currentTime > 0) {
        this.player.currentTime = currentTime;
      }
      if (!paused) {
        this.player.play();
      }
    });
  }

  // 将播放器切换到视频模式
  toVideo() {
    if (this.player.media instanceof HTMLVideoElement) {
      this.debug("Already in video mode, no need to switch.");
      return;
    }

    // 保存当前播放状态
    let currentTime = 0;
    let volume = 1;
    let playbackRate = 1;
    let muted = false;
    let paused = true;

    if (this.player) {
      currentTime = this.player.currentTime || 0;
      volume = this.player.volume || 1;
      playbackRate = this.player.playbackRate || 1;
      muted = this.player.muted || false;
      paused = this.player.paused;

      // 销毁当前播放器
      this.player.destroy();
    }

    // 创建视频模式的选项 (移除 music 属性)
    const videoOptions: XgplayerOptions = {
      ...this.initOptions,
      volume: volume,
      playbackRate: playbackRate,
      muted: muted,
    };

    // 移除音乐模式相关属性
    delete videoOptions.music;
    console.log("toVideo", videoOptions);
    // 重新初始化为视频模式
    this.init(this.player.url, videoOptions);

    // 在播放器准备好后恢复播放状态
    this.player.once(Events.READY, () => {
      if (currentTime > 0) {
        this.player.currentTime = currentTime;
      }
      if (!paused) {
        this.player.play();
      }
    });
  }

  private init(url: IPlayerOptions["url"], options: XgplayerOptions) {
    let playerOptions: IPlayerOptions = {
      el: this.el,
      url,
    };

    if (
      options.fullscreenTarget &&
      typeof options.fullscreenTarget === "string"
    ) {
      const fullscreenEl = document.querySelector(options.fullscreenTarget);
      if (fullscreenEl) {
        options.fullscreenTarget = fullscreenEl;
      }
    }

    if (
      options.cssFullscreen &&
      options.cssFullscreen.target &&
      typeof options.cssFullscreen.target === "string"
    ) {
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

    this.player.on(Events.READY, () => {
      this.debug("Player is ready");
      this.handle.invokeMethodAsync("OnReady");
    });
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
    this.player.on(Events.ENDED, val => {
      this.debug("Video ended", val);
      this.handle.invokeMethodAsync("OnEnded");
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
      console.debug(`[Xgplayer] ${message}`, ...args);
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
