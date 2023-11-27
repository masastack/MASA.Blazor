// Called when the script first gets loaded on the page.
export function onLoad() {
    console.log('Load');
}

// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
export function onUpdate() {
    console.log('Update');
}

// Called when an enhanced page update removes the script from the page.
export function onDispose() {
    console.log('Dispose');
}
