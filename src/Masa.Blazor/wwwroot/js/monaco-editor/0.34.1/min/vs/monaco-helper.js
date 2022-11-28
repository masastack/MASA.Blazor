window.MasaBlazor = window.MasaBlazor || {};
window.MasaBlazor.Editors = [];

export function Init(id, options) {
    let thisEditor = monaco.editor.create(document.getElementById(id), options);
    if (window.MasaBlazor.Editors.find(e => e.id === id)) {
        return false;
    }
    else {
        window.MasaBlazor.Editors.push({ id: id, editor: thisEditor });
    }
    return true;
}

export function GetValue(id) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    return myEditor.editor.getValue();
}
export function SetValue(id, value) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.setValue(value);
    return true;
}
export function SetTheme(id, theme) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.setTheme(theme);
    return true;
}

export function GetModels(id) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    return myEditor.editor.getModels();
}

export function getModel(id, uri) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    return myEditor.editor.getModel(uri);
}
export function setModelLanguage(id, model, languageId) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.setModelLanguage(model, languageId);
}
export function remeasureFonts(id) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.remeasureFonts();
}

export function addKeybindingRules(id, rules) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.addKeybindingRules(rules);
}

export function addKeybindingRule(id, rule) {
    let myEditor = window.MasaBlazor.Editors.find(e => e.id === id);
    if (!myEditor) {
        throw `Could not find a editor with id: '${window.MasaBlazor.Editors.length}' '${id}'`;
    }
    myEditor.editor.addKeybindingRule(rule);
}
