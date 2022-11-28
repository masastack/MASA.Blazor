window.Masa = window.Masa || {};
window.Masa.Editors = [];

window.Masa.Editor = {
    Init: function (id, options) {
        let thisEditor = monaco.editor.create(document.getElementById(id), options);
        if (window.Masa.Editors.find(e => e.id === id)) {
            return false;
        }
        else {
            window.Masa.Editors.push({ id: id, editor: thisEditor });
        }
        return true;
    },
    GetValue: function (id) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        return myEditor.editor.getValue();
    },
    SetValue: function (id, value) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.setValue(value);
        return true;
    },
    SetTheme: function (id, theme) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.setTheme(theme);
        return true;
    },
    GetModels: function (id) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        return myEditor.editor.getModels();
    },
    getModel: function (id, uri) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        return myEditor.editor.getModel(uri);
    },
    setModelLanguage: function (id, model, languageId) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.setModelLanguage(model, languageId);
    },
    remeasureFonts: function (id) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.remeasureFonts();
    },
    addKeybindingRules: function (id, rules) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.addKeybindingRules(rules);
    },
    addKeybindingRule: function (id, rule) {
        let myEditor = window.Masa.Editors.find(e => e.id === id);
        if (!myEditor) {
            throw `Could not find a editor with id: '${window.Masa.Editors.length}' '${id}'`;
        }
        myEditor.editor.addKeybindingRule(rule);
    }
}
