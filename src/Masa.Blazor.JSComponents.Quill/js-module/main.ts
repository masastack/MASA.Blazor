import QuillClass, { Parchment, QuillOptions } from "quill";

declare const Quill: QuillClass;

export function create(container: HTMLElement, options: QuillOptions) {
  return new (Quill as any)(container, options);
}
