import "core-js/modules/es.array.at.js";
import "core-js/modules/es.string.replace-all";
import "core-js/modules/web.structured-clone.js";
import "core-js/proposals/promise-with-resolvers";

import {
    build, getDocument, GlobalWorkerOptions, InvalidPDFException, MissingPDFException,
    PDFDocumentProxy, UnexpectedResponseException, version
} from "pdfjs-dist";
import * as pdfjsViewer from "pdfjs-dist/web/pdf_viewer.mjs";

const TEXT_LAYER_MODE = 0; // DISABLE
const MAX_IMAGE_SIZE = 1024 * 1024;
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
  previous: HTMLElement;
  next: HTMLElement;
  zoomInBtn: HTMLElement;
  zoomOutBtn: HTMLElement;
  container: HTMLDivElement;
  viewerDiv: HTMLElement;
  pagination: HTMLElement;
  maxCanvasPixels: number;
  maxImageSize: number;
  startDistance = 0;

  _zoomIn = () => this.zoomIn();

  _zoomOut = () => this.zoomOut();

  _previousClick = () => this.page--;

  _nextClick = () => this.page++;

  _touchstart = (e: TouchEvent) => {
    if (e.touches.length === 2) {
      this.startDistance = getDistance(e.touches);
      console.log("touchstart startDistance", this.startDistance);
      e.preventDefault();
    }
  };

  _touchmove = (e: TouchEvent) => {
    if (e.touches.length === 2) {
      const currentDistance = getDistance(e.touches);
      if (Math.abs(currentDistance - this.startDistance) > 10) {
        const scaleChange = currentDistance / this.startDistance;
        this.startDistance = currentDistance;
        const origin = getCenter(e.touches);
        if (scaleChange > 1) {
          this.zoomIn(origin);
        } else {
          this.zoomOut(origin);
        }
      }

      e.preventDefault();
    }
  };

  _wheel = (e: WheelEvent) => {
    if (e.ctrlKey) {
      e.preventDefault();

      const origin = [e.clientX, e.clientY];

      if (e.deltaY < 0) {
        this.zoomIn(origin);
      } else {
        this.zoomOut(origin);
      }
    }
  };

  constructor(container: HTMLDivElement, maxCanvasPixels: number) {
    this.container = container;
    this.maxCanvasPixels = maxCanvasPixels;
    this.previous = container.querySelector(".previous");
    this.next = container.querySelector(".next");
    this.zoomInBtn = container.querySelector(".zoomIn");
    this.zoomOutBtn = container.querySelector(".zoomOut");
    this.viewerDiv = this.container.firstElementChild as HTMLElement;
    this.pagination = container.querySelector(".pagination");

    this.pdfLoadingTask = null;
    this.pdfDocument = null;
    this.pdfViewer = null;
    this.pdfHistory = null;
    this.pdfLinkService = null;
    this.eventBus = null;
    this.value = 0;
  }

  async open(url: string, maxImageSize: number) {
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
      maxImageSize,
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

  zoomIn(origin: any[] = null) {
    this.pdfViewer.updateScale({
      scaleFactor: 1.1,
      origin,
    });
  }

  zoomOut(origin: any[] = null) {
    this.pdfViewer.updateScale({
      scaleFactor: 0.9,
      origin,
    });
  }

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
      maxCanvasPixels: this.maxCanvasPixels,
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
      if (this.pagination) {
        this.pagination.textContent = `${pdfViewer.currentPageNumber} / ${this.pagesCount}`;
      }
    });

    eventBus.on(
      "pagechanging",
      (evt) => {
        const page = evt.pageNumber;
        const numPages = this.pagesCount;
        if (this.pagination) {
          this.pagination.textContent = `${page} / ${numPages}`;
        }
      },
      true
    );

    this.container.addEventListener("touchstart", this._touchstart, {
      passive: false,
    });

    this.container.addEventListener("touchmove", this._touchmove, {
      passive: false,
    });

    this.viewerDiv.addEventListener("wheel", this._wheel);
    this.previous &&
      this.previous.addEventListener("click", this._previousClick);
    this.next && this.next.addEventListener("click", this._nextClick);
    this.zoomInBtn && this.zoomInBtn.addEventListener("click", this._zoomIn);
    this.zoomOutBtn && this.zoomOutBtn.addEventListener("click", this._zoomOut);
  }

  destroy() {
    this.container.removeEventListener("touchstart", this._touchstart);
    this.container.removeEventListener("touchmove", this._touchmove);
    this.viewerDiv.removeEventListener("wheel", this._wheel);

    this.previous &&
      this.previous.removeEventListener("click", this._previousClick);
    this.next && this.next.removeEventListener("click", this._nextClick);
    this.zoomInBtn && this.zoomInBtn.removeEventListener("click", this._zoomIn);
    this.zoomOutBtn &&
      this.zoomOutBtn.removeEventListener("click", this._zoomOut);
  }
}

function getCenter(touches: TouchList) {
  const centerX = (touches[0].clientX + touches[1].clientX) / 2;
  const centerY = (touches[0].clientY + touches[1].clientY) / 2;
  return [centerX, centerY];
}

function getDistance(touches: TouchList) {
  const dx = touches[0].pageX - touches[1].pageX;
  const dy = touches[0].pageY - touches[1].pageY;
  return Math.sqrt(dx * dx + dy * dy);
}

function init(
  viewerContainer: HTMLDivElement,
  url: string,
  maxCanvasPixels: number = 0,
  maxImageSize: number = MAX_IMAGE_SIZE
) {
  const pdfViewerApp = new PDFViewerApplication(
    viewerContainer,
    maxCanvasPixels,
  );

  GlobalWorkerOptions.workerSrc = new URL(
    "./pdf.worker.js",
    import.meta.url
  ).toString();
  pdfViewerApp.initUI();
  pdfViewerApp.open(url, maxImageSize);

  return pdfViewerApp;
}

export { init };
