
let textareaAutoGrowNextId = 0;
const textareaAutoGrowMap: {
    [prop: string]: (e: HTMLElementEventMap['input']) => void;
} = {}

export function registerTextareaAutoGrowEvent(input: HTMLElement) {
    const id = textareaAutoGrowNextId;
    const inputCallback = (e: HTMLElementEventMap['input']) => {
        const target = e.target as HTMLTextAreaElement;
        const autoGrow = target.getAttribute('data-auto-grow');
        if (autoGrow === undefined) {
            return;
        }
        const rows = target.getAttribute('rows');
        const rowHeight = target.getAttribute('data-row-height');
        calculateTextareaHeight(target, rows, rowHeight)
    };

    textareaAutoGrowMap[id] = inputCallback;

    input.addEventListener('input', inputCallback)

    return textareaAutoGrowNextId++;
}

export function unregisterTextareaAutoGrowEvent(input: HTMLElement, textareaAutoGrowNextId: number) {
    if (!input) return;
    const inputCallback = textareaAutoGrowMap[textareaAutoGrowNextId];
    if (inputCallback) {
        input.removeEventListener('input', inputCallback);
    }
}

export function calculateTextareaHeight(textarea: HTMLTextAreaElement, rows: string, rowHeight: string) {
    textarea.style.height = '0'
    const height = textarea.scrollHeight;
    const minHeight = parseInt(rows, 10) * parseFloat(rowHeight);
    textarea.style.height = Math.max(height, minHeight) + 'px';
}