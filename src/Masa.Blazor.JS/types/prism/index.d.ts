declare namespace Prism {
  export interface LanguageMap {
    [language: string]: any;
  }
}

declare interface Prism {
  languages: Prism.LanguageMap;
  highlight(text: string, grammar: any, language: string): string;
}
