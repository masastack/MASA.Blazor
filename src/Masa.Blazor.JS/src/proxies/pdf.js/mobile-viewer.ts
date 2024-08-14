import {
    build, getDocument, GlobalWorkerOptions, InvalidPDFException, MissingPDFException,
    PDFDocumentProxy, shadow, UnexpectedResponseException, version
} from "pdfjs-dist";
import * as pdfjsViewer from "pdfjs-dist/legacy/web/pdf_viewer.mjs";

const MAX_CANVAS_PIXELS = 0; // CSS-only zooming.
const TEXT_LAYER_MODE = 0; // DISABLE
const MAX_IMAGE_SIZE = 1024 * 1024;
// const CMAP_URL = "../../node_modules/pdfjs-dist/cmaps/";
// const CMAP_PACKED = true;

const DEFAULT_SCALE_DELTA = 1.1;
const MIN_SCALE = 0.25;
const MAX_SCALE = 10.0;
const DEFAULT_SCALE_VALUE = "auto";

class PDFViewerApplication {
  pdfLoadingTask: any;
  pdfDocument: PDFDocumentProxy;
  pdfViewer: pdfjsViewer.PDFViewer;
  pdfHistory: any;
  pdfLinkService: any;
  eventBus: any;
  value: number;
  l10n: pdfjsViewer.GenericL10n;
  root: HTMLElement;
  previous: HTMLElement;
  next: HTMLElement;
  zoomInBtn: HTMLElement;
  zoomOutBtn: HTMLElement;
  container: HTMLDivElement;
  viewerDiv: HTMLElement;
  pagination: HTMLElement;

  constructor(root: HTMLElement) {
    this.root = root;
    this.previous = root.querySelector(".previous");
    this.next = root.querySelector(".next");
    this.zoomInBtn = root.querySelector(".zoomIn");
    this.zoomOutBtn = root.querySelector(".zoomOut");
    this.container = root.querySelector(".viewerContainer");
    this.viewerDiv = this.container.firstElementChild as HTMLElement;
    this.pagination = root.querySelector(".pagination");

    this.pdfLoadingTask = null;
    this.pdfDocument = null;
    this.pdfViewer = null;
    this.pdfHistory = null;
    this.pdfLinkService = null;
    this.eventBus = null;
    this.value = 0;
  }

  async open(url: string) {
    if (this.pdfLoadingTask) {
      // We need to destroy already opened document
      return this.close().then(
        function () {
          // ... and repeat the open() call.
          return this.open(url);
        }.bind(this)
      );
    }

    const self = this;

    // Loading document.
    const loadingTask = getDocument({
      url,
      maxImageSize: MAX_IMAGE_SIZE,
    });
    this.pdfLoadingTask = loadingTask;

    loadingTask.onProgress = function (progressData) {
      self.progress(progressData.loaded / progressData.total);
    };

    try {
      const pdfDocument = await loadingTask.promise;
      this.pdfDocument = pdfDocument;
      this.pdfViewer.setDocument(pdfDocument);
      this.pdfLinkService.setDocument(pdfDocument);
      this.pdfHistory.initialize({
        fingerprint: pdfDocument.fingerprints[0],
      });
      // this.loadingBar.hide();
    } catch (reason) {
      let key = "pdfjs-loading-error";
      if (reason instanceof InvalidPDFException) {
        key = "pdfjs-invalid-file-error";
      } else if (reason instanceof MissingPDFException) {
        key = "pdfjs-missing-file-error";
      } else if (reason instanceof UnexpectedResponseException) {
        key = "pdfjs-unexpected-response-error";
      }
      self.l10n.get(key, null, null).then((msg) => {
        self.error(msg, { message: reason?.message });
      });
      // self.loadingBar.hide();
    }
  }

  async close() {
    if (!this.pdfLoadingTask) {
      return;
    }

    await this.pdfLoadingTask.destroy();
    this.pdfLoadingTask = null;

    if (this.pdfDocument) {
      this.pdfDocument = null;

      this.pdfViewer.setDocument(null);
      this.pdfLinkService.setDocument(null, null);

      if (this.pdfHistory) {
        this.pdfHistory.reset();
      }
    }
  }

  // get loadingBar() {
  //   const bar = document.getElementById("loadingBar");
  //   return shadow(this, "loadingBar", new pdfjsViewer.ProgressBar(bar));
  // }

  error(message, moreInfo) {
    const moreInfoText = [`PDF.js v${version || "?"} (build: ${build || "?"})`];
    if (moreInfo) {
      moreInfoText.push(`Message: ${moreInfo.message}`);

      if (moreInfo.stack) {
        moreInfoText.push(`Stack: ${moreInfo.stack}`);
      } else {
        if (moreInfo.filename) {
          moreInfoText.push(`File: ${moreInfo.filename}`);
        }
        if (moreInfo.lineNumber) {
          moreInfoText.push(`Line: ${moreInfo.lineNumber}`);
        }
      }
    }

    console.error(`${message}\n\n${moreInfoText.join("\n")}`);
  }

  progress(level) {
    const percent = Math.round(level * 100);
    // Updating the bar if value increases.
    // if (percent > this.loadingBar.percent || isNaN(percent)) {
    //   this.loadingBar.percent = percent;
    // }
  }

  get pagesCount() {
    return this.pdfDocument.numPages;
  }

  get page() {
    return this.pdfViewer.currentPageNumber;
  }

  set page(val) {
    this.pdfViewer.currentPageNumber = val;
  }

  _zoomIn = () => this.zoomIn();
  _zoomOut = () => this.zoomOut();
  _previousClick = () => this.page--;
  _nextClick = () => this.page++;

  zoomIn(ticks = undefined) {
    let newScale = this.pdfViewer.currentScale;
    do {
      newScale = Number((newScale * DEFAULT_SCALE_DELTA).toFixed(2));
      newScale = Math.ceil(newScale * 10) / 10;
      newScale = Math.min(MAX_SCALE, newScale);
    } while (--ticks && newScale < MAX_SCALE);
    this.pdfViewer.currentScaleValue = newScale.toFixed(2);
  }

  zoomOut(ticks = undefined) {
    let newScale = this.pdfViewer.currentScale;
    do {
      newScale = Number((newScale / DEFAULT_SCALE_DELTA).toFixed(2));
      newScale = Math.floor(newScale * 10) / 10;
      newScale = Math.max(MIN_SCALE, newScale);
    } while (--ticks && newScale > MIN_SCALE);
    this.pdfViewer.currentScaleValue = newScale.toFixed(2);
  }

  previousPage() {}

  initUI() {
    const eventBus = new pdfjsViewer.EventBus();
    this.eventBus = eventBus;

    const linkService = new pdfjsViewer.PDFLinkService({
      eventBus,
    });
    this.pdfLinkService = linkService;

    this.l10n = new pdfjsViewer.GenericL10n(null);

    const pdfViewer = new pdfjsViewer.PDFViewer({
      container: this.container,
      eventBus,
      linkService,
      l10n: this.l10n,
      maxCanvasPixels: MAX_CANVAS_PIXELS,
      textLayerMode: TEXT_LAYER_MODE,
    });
    this.pdfViewer = pdfViewer;
    linkService.setViewer(pdfViewer);

    this.pdfHistory = new pdfjsViewer.PDFHistory({
      eventBus,
      linkService,
    });
    linkService.setHistory(this.pdfHistory);

    eventBus.on("pagesinit", () => {
      // We can use pdfViewer now, e.g. let's change default scale.
      pdfViewer.currentScaleValue = DEFAULT_SCALE_VALUE;
      this.pagination.textContent = `${pdfViewer.currentPageNumber} / ${this.pagesCount}`;
    });

    eventBus.on(
      "pagechanging",
      (evt) => {
        const page = evt.pageNumber;
        const numPages = this.pagesCount;
        this.pagination.textContent = `${page} / ${numPages}`;
      },
      true
    );

    // container.firstElementChild.addEventListener(
    //   "touchstart",
    //   (e: TouchEvent) => {
    //     if (e.touches.length === 2) {
    //       this.startDistance = this.getDistance(e.touches);
    //       console.log("touchstart startDistance", this.startDistance);
    //       e.preventDefault();
    //     }
    //   },
    //   { passive: false }
    // );

    // container.firstElementChild.addEventListener(
    //   "touchmove",
    //   (e: TouchEvent) => {
    //     if (e.touches.length === 2) {
    //       const currentDistance = this.getDistance(e.touches);
    //       console.log("touchmove currentDistance", currentDistance);
    //       const scaleChange = currentDistance / this.startDistance;
    //       console.log(
    //         "old this.pdfViewer.currentScaleValue",
    //         this.pdfViewer.currentScaleValue
    //       );
    //       this.pdfViewer.currentScaleValue *= scaleChange;
    //       console.log(
    //         "new this.pdfViewer.currentScaleValue",
    //         this.pdfViewer.currentScaleValue
    //       );
    //       this.startDistance = currentDistance;

    //       e.preventDefault();
    //     }
    //   },
    //   { passive: false }
    // );

    this.viewerDiv.addEventListener("wheel", this._wheel);
    this.previous &&
      this.previous.addEventListener("click", this._previousClick);
    this.next && this.next.addEventListener("click", this._nextClick);
    this.zoomInBtn && this.zoomInBtn.addEventListener("click", this._zoomIn);
    this.zoomOutBtn && this.zoomOutBtn.addEventListener("click", this._zoomOut);
  }

  startDistance = 0;

  _wheel = (e: WheelEvent) => {
    if (e.ctrlKey) {
      e.preventDefault();
      if (e.deltaY < 0) {
        this.zoomIn();
      } else {
        this.zoomOut();
      }
    }
  };

  getDistance(touches) {
    const dx = touches[0].pageX - touches[1].pageX;
    const dy = touches[0].pageY - touches[1].pageY;
    return Math.sqrt(dx * dx + dy * dy);
  }

  destroy() {
    this.viewerDiv.removeEventListener("wheel", this._wheel);

    this.previous &&
      this.previous.removeEventListener("click", this._previousClick);
    this.next && this.next.removeEventListener("click", this._nextClick);
    this.zoomInBtn && this.zoomInBtn.removeEventListener("click", this._zoomIn);
    this.zoomOutBtn &&
      this.zoomOutBtn.removeEventListener("click", this._zoomOut);
  }
}

function init(root: HTMLElement, url: string) {
  const pdfViewerApp = new PDFViewerApplication(root);

  GlobalWorkerOptions.workerSrc = new URL(
    "./pdf.worker.min.js",
    import.meta.url
  ).toString();
  pdfViewerApp.initUI();
  pdfViewerApp.open(url);

  return pdfViewerApp;
}

export { init };
