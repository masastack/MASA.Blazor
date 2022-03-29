const isMobile = window.innerWidth <= 960;
const defaultOptions = {
    width: isMobile ? '100%' : '80%',
    height: '0',
    tab: '\t',
    counter: '999999',
    typewriterMode: true,
    mode: 'sv',
    preview: {
        delay: 100,
        show: !isMobile
    },
    outline: true,
    cache: {
        enable: false,
    },
}


export function init(domRef, obj, value, options) {
    if (options&&options.hasOwnProperty('toolbar')) {
        options.toolbar.forEach(btn => {
            if (typeof btn == 'object') {
                btn.click = () => {
                    obj.invokeMethodAsync('HandleToolbarButtonClick', btn.name);
                }
            }
        })
    }
    domRef.Vditor = new Vditor(domRef, {
        ...defaultOptions,
        ...options,
        value,
        after: () => {
            obj.invokeMethodAsync('HandleRendered', value);
        },
        input: (value) => {
            obj.invokeMethodAsync('HandleInput', value);
        },
        focus: (value) => {
            obj.invokeMethodAsync('HandleFocus', value);
        },
        blur: (value) => {
            obj.invokeMethodAsync('HandleBlur', value);
        },
        esc: (value) => {
            obj.invokeMethodAsync('HandleEscPress', value);
        },
        ctrlEnter: (value) => {
            obj.invokeMethodAsync('HandleCtrlEnterPress', value);
        },
        select: (value) => {
            editor.invokeMethodAsync('HandleSelect', value);
        },
    })
}
export function getValue(domRef) {
    return domRef.Vditor.getValue();
}
export function getHtml(domRef) {
    return domRef.Vditor.getHTML();
}
export function setValue(domRef, value, clearStack = false) {
    domRef.Vditor.setValue(value, clearStack);
}
export function insertValue(domRef, value, render = true) {
    domRef.Vditor.insertValue(value, render);
}
export function destroy(domRef) {
    domRef.Vditor.destroy();
}
export function preview(domRef, editor, markdown, options = {}) {
    Vditor.preview(domRef, markdown, {
        ...options,
        after() {
            if (options.handleAfter) {
                editor.invokeMethodAsync('HandleAfter');
            }
        }
    });
}