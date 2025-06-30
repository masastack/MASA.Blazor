class VideoFeed {
  private videoSelector: string;
  private videoEl: HTMLVideoElement & {
    webkitRequestFullscreen?: () => void;
    mozRequestFullScreen?: () => void;
    msRequestFullscreen?: () => void;
  };
  private dotNetHelper: DotNet.DotNetObject;
  private onLoadedMetadataHandler: () => void;
  private onTimeUpdateHandler: (e: Event) => void;

  constructor(videoSelector: string, dotNetHelper: DotNet.DotNetObject) {
    this.videoSelector = videoSelector;
    this.dotNetHelper = dotNetHelper;
    this.videoEl = document.querySelector(videoSelector);
  }

  init() {
    let duration,
      aspectRatio = 1;
    if (this.videoEl.duration) {
      duration = this.videoEl.duration;
      aspectRatio = this.videoEl.videoWidth / this.videoEl.videoHeight;
    } else {
      // if the video is not loaded yet, use event listener to get metadata
      this.onLoadedMetadataHandler = () => {
        duration = this.videoEl.duration;
        aspectRatio = this.videoEl.videoWidth / this.videoEl.videoHeight;
        this.dotNetHelper.invokeMethodAsync("OnLoadedMetadata", {
          duration,
          aspectRatio,
        });
      };

      this.videoEl.addEventListener(
        "loadedmetadata",
        this.onLoadedMetadataHandler
      );
    }

    this.onTimeUpdateHandler = (e) => {
      this.dotNetHelper.invokeMethodAsync(
        "OnTimeUpdate",
        this.videoEl.currentTime
      );
    };

    this.videoEl.addEventListener("timeupdate", this.onTimeUpdateHandler);

    this.videoEl.play();

    return { duration: duration ?? 0, aspectRatio };
  }

  update(currentTime: number) {
    this.videoEl.currentTime = currentTime;
  }

  requestFullscreen() {
    if (this.videoEl.requestFullscreen) {
      this.videoEl.requestFullscreen();
    } else if (this.videoEl.webkitRequestFullscreen) {
      this.videoEl.webkitRequestFullscreen();
    } else if (this.videoEl.mozRequestFullScreen) {
      this.videoEl.mozRequestFullScreen();
    } else if (this.videoEl.msRequestFullscreen) {
      this.videoEl.msRequestFullscreen();
    }
  }

  toggleMute(force?: boolean) {
    if (force !== undefined) {
      this.videoEl.muted = force;
    } else {
      this.videoEl.muted = !this.videoEl.muted;
    }
  }

  pause() {
    this.videoEl.pause();
  }

  play() {
    this.videoEl.play();
  }

  dispose() {
    if (this.onLoadedMetadataHandler) {
      this.videoEl.removeEventListener(
        "loadedmetadata",
        this.onLoadedMetadataHandler
      );
    }

    if (this.onTimeUpdateHandler) {
      this.videoEl.removeEventListener("timeupdate", this.onTimeUpdateHandler);
    }

    this.dotNetHelper = null;
  }
}

export function create(
  id: string,
  dotNetHelper: DotNet.DotNetObject
): VideoFeed {
  return new VideoFeed("#" + id, dotNetHelper);
}
