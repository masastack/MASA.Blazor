document.addEventListener('swUpdate', function (e) {
    setTimeout(() => {
        const pwaSnackbar = document.querySelector(".m-snackbar--pwa > .m-snack__wrapper");
        if (!pwaSnackbar) return;

        pwaSnackbar.style.display = "flex";
    }, 5000)
});

window.swSkipWaiting = function () {
    navigator.serviceWorker.getRegistration().then(function (reg) {
        reg.waiting.postMessage({type: 'SKIP_WAITING'});
    });
}

window.swIgnoreUpdate = function () {
    const pwaSnackbar = document.querySelector(".m-snackbar--pwa > .m-snack__wrapper");
    console.log("ignore", pwaSnackbar, pwaSnackbar.style)
    pwaSnackbar.style.display = "none";
}

window.emitSwUpdate = function () {
    const event = new CustomEvent('swUpdate');
    document.dispatchEvent(event);
};

if ('serviceWorker' in navigator) {
    navigator.serviceWorker.addEventListener('controllerchange', function () {
        window.location.reload();
    });

    navigator.serviceWorker.register('service-worker.js', {updateViaCache: 'none'}).then(function (reg) {
        if (reg.waiting) {
            emitSwUpdate();
            return;
        }

        reg.onupdatefound = function () {
            const installingWorker = reg.installing;
            installingWorker.onstatechange = function () {
                switch (installingWorker.state) {
                    case 'installed':
                        if (navigator.serviceWorker.controller) {
                            emitSwUpdate();
                        }
                        break;
                }
            };
        };
    }, /*catch*/ function (error) {
        console.error('Service worker registration failed:', error);
    });
}