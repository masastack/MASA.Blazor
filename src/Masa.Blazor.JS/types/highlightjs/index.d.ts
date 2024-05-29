declare namespace hljs {
  export interface HighlightOptions {
    language: string;
    ignoreIllegals?: boolean;
  }

  export interface HighlightResult {
    code?: string;
    relevance: number;
    value: string;
    language?: string;
    illegal: boolean;
    errorRaised?: Error;
    // * for auto-highlight
    secondBest?: Omit<HighlightResult, "second_best">;
  }
}

declare interface hljs {
  getLanguage: (languageName: string) => any;
  highlight(code: string, options: hljs.HighlightOptions): hljs.HighlightResult;
}
