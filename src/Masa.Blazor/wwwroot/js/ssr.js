// Called when the script first gets loaded on the page.
export function onLoad() {
    console.log('Load');
}

// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
export function onUpdate() {
    console.log('Update');

    them();
    
    
}

// Called when an enhanced page update removes the script from the page.
export function onDispose() {
    console.log('Dispose');
}

function them() {
    console.log('initTheme: change the theme css of .m-application')
    BlazorComponent.interop.ssr.initTheme()
}