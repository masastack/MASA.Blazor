import { editor as MonacoEditor } from "monaco-editor";

interface Monaco {
  editor: typeof MonacoEditor;
  KeyMod: any;
  KeyCode: any;
}

declare const monaco: Monaco;

function init(el: HTMLElement, options: MonacoEditor.IStandaloneEditorConstructionOptions, dotNetHelper) {
  const editor = monaco.editor.create(el, options);
  editor.onDidChangeModelContent(()=> {
    dotNetHelper.invokeMethodAsync("OnChange", editor.getValue());
  });

  return editor;
}

function defineTheme(name, value: MonacoEditor.IStandaloneThemeData) {
  monaco.editor.defineTheme(name, value)
}

function remeasureFonts() {
  monaco.editor.remeasureFonts();
}

function addKeybindingRules(rules: MonacoEditor.IKeybindingRule[]) {
  monaco.editor.addKeybindingRules(rules);
}

function addKeybindingRule(rule: MonacoEditor.IKeybindingRule) {
  monaco.editor.addKeybindingRule(rule);
}

function setTheme(theme: string) {
  monaco.editor.setTheme(theme);
}

function colorizeElement(id: string, options: any) {
  monaco.editor.colorizeElement(document.getElementById(id), options);
}

function getModels() {
  return monaco.editor.getModels();
}

function updateOptions(instance: MonacoEditor.IStandaloneCodeEditor, options: any) {
  instance.updateOptions(options);
}

function addCommand(
  instance: MonacoEditor.IStandaloneCodeEditor,
  keybinding: number,
  dotNetHelper,
  method: string
) {
  instance.addCommand(keybinding, function () {
    dotNetHelper.invokeMethodAsync(method);
  });
}

function getModel(instance: MonacoEditor.IStandaloneCodeEditor) {
  return instance.getModel();
}

function getValue(instance: MonacoEditor.IStandaloneCodeEditor) {
  return instance.getValue();
}

function setValue(instance: MonacoEditor.IStandaloneCodeEditor, value) {
  instance.setValue(value);
}

function setModelLanguage(instance: MonacoEditor.IStandaloneCodeEditor, languageId: string) {
  monaco.editor.setModelLanguage(instance.getModel(), languageId);
}

export {
  init,
  getValue,
  setValue,
  setTheme,
  getModels,
  getModel,
  setModelLanguage,
  remeasureFonts,
  addKeybindingRules,
  colorizeElement,
  defineTheme,
  addCommand,
  updateOptions,
  addKeybindingRule
}