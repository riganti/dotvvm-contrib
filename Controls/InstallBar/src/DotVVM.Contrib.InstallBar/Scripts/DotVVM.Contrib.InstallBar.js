export default context => new InstallBar(context);

const visibleClass = "dotvvm-contrib-install-bar--visible";
const documentVisibleClass = "dotvvm-contrib-install-bar-visible";
const heightVariable = "--dotvvm-contrib-install-bar-height";
const modes = ["prompt", "ios", "manual"];

let currentInstance = null;

function isStandalone() {
    return ["standalone", "fullscreen", "minimal-ui", "window-controls-overlay"]
        .some(mode => window.matchMedia && window.matchMedia(`(display-mode: ${mode})`).matches)
        || window.navigator.standalone === true
        || document.referrer.startsWith("android-app://");
}

function isIos() {
    return /iPad|iPhone|iPod/.test(window.navigator.userAgent)
        || (window.navigator.platform === "MacIntel" && window.navigator.maxTouchPoints > 1);
}

function getDeferredPrompt() {
    return window.__dotvvmContribInstallPrompt || null;
}

function readSessionStorage(key) {
    try {
        return window.sessionStorage.getItem(key);
    } catch {
        return null;
    }
}

function writeSessionStorage(key, value) {
    try {
        if (value === null) {
            window.sessionStorage.removeItem(key);
        } else {
            window.sessionStorage.setItem(key, value);
        }
    } catch {
        // session storage may be unavailable (private mode, blocked storage)
    }
}

class InstallBar {

    constructor(context) {
        this.context = context;

        const first = context.elements[0];
        this.element = (first.classList && first.classList.contains("dotvvm-contrib-install-bar"))
            ? first
            : first.parentElement.querySelector(".dotvvm-contrib-install-bar");
        this.storageKey = this.element.dataset.storageKey;
        this.installButton = this.element.querySelector(".dotvvm-contrib-install-bar__install");
        this.closeButton = this.element.querySelector(".dotvvm-contrib-install-bar__close");

        this.onInstallClick = () => this.install();
        this.onCloseClick = () => this.dismiss();
        this.onPromptAvailable = () => this.autoShow();
        this.onAppInstalled = () => this.hide();
        this.onDisplayModeChange = () => { if (isStandalone()) this.hide(); };

        this.installButton.addEventListener("click", this.onInstallClick);
        this.closeButton.addEventListener("click", this.onCloseClick);
        window.addEventListener("dotvvm-contrib-install-prompt", this.onPromptAvailable);
        window.addEventListener("dotvvm-contrib-app-installed", this.onAppInstalled);

        this.displayModeQuery = window.matchMedia ? window.matchMedia("(display-mode: standalone)") : null;
        if (this.displayModeQuery && this.displayModeQuery.addEventListener) {
            this.displayModeQuery.addEventListener("change", this.onDisplayModeChange);
        }

        if (window.ResizeObserver) {
            this.resizeObserver = new ResizeObserver(() => this.updateHeight());
            this.resizeObserver.observe(this.element);
        }

        currentInstance = this;
        registerGlobalApi();

        this.autoShow();
    }

    /** Shows the bar automatically, unless the app is installed, the user closed the bar, or there is no way to install the app. */
    autoShow() {
        if (isStandalone() || readSessionStorage(this.storageKey)) {
            return;
        }
        const mode = this.getMode();
        if (mode === "prompt" || mode === "ios") {
            this.show(mode);
        }
    }

    getMode() {
        if (getDeferredPrompt()) {
            return "prompt";
        }
        return isIos() ? "ios" : "manual";
    }

    show(mode) {
        if (isStandalone()) {
            return false;
        }

        mode = mode || this.getMode();
        for (const m of modes) {
            this.element.classList.toggle("dotvvm-contrib-install-bar--" + m, m === mode);
        }
        this.installButton.hidden = mode !== "prompt";

        this.element.hidden = false;
        // force a reflow so the slide-in transition starts from the hidden position
        void this.element.offsetHeight;
        this.element.classList.add(visibleClass);
        document.documentElement.classList.add(documentVisibleClass);
        this.updateHeight();
        return true;
    }

    hide() {
        this.element.classList.remove(visibleClass);
        this.element.hidden = true;
        document.documentElement.classList.remove(documentVisibleClass);
        document.documentElement.style.removeProperty(heightVariable);
    }

    /** Hides the bar and remembers the decision for the rest of the browser session. */
    dismiss() {
        writeSessionStorage(this.storageKey, "true");
        this.hide();
    }

    async install() {
        const deferredPrompt = getDeferredPrompt();
        if (!deferredPrompt) {
            this.show();
            return false;
        }

        // the prompt can be used only once
        window.__dotvvmContribInstallPrompt = null;
        deferredPrompt.prompt();
        const choice = await deferredPrompt.userChoice;
        if (choice.outcome === "accepted") {
            this.hide();
            return true;
        } else {
            this.dismiss();
            return false;
        }
    }

    updateHeight() {
        if (!this.element.hidden) {
            document.documentElement.style.setProperty(heightVariable, this.element.offsetHeight + "px");
        }
    }

    $dispose() {
        this.installButton.removeEventListener("click", this.onInstallClick);
        this.closeButton.removeEventListener("click", this.onCloseClick);
        window.removeEventListener("dotvvm-contrib-install-prompt", this.onPromptAvailable);
        window.removeEventListener("dotvvm-contrib-app-installed", this.onAppInstalled);
        if (this.displayModeQuery && this.displayModeQuery.removeEventListener) {
            this.displayModeQuery.removeEventListener("change", this.onDisplayModeChange);
        }
        if (this.resizeObserver) {
            this.resizeObserver.disconnect();
        }

        if (currentInstance === this) {
            document.documentElement.classList.remove(documentVisibleClass);
            document.documentElement.style.removeProperty(heightVariable);
            currentInstance = null;
        }
    }
}

function registerGlobalApi() {
    window.DotVVM = window.DotVVM || {};
    window.DotVVM.Contrib = window.DotVVM.Contrib || {};
    window.DotVVM.Contrib.InstallBar = window.DotVVM.Contrib.InstallBar || {
        /** Shows the install bar again (e.g. from a menu item), even if the user closed it before. Returns false if the app already runs in the standalone mode. */
        show: () => {
            if (!currentInstance || isStandalone()) {
                return false;
            }
            writeSessionStorage(currentInstance.storageKey, null);
            return currentInstance.show();
        },
        /** Hides the install bar without remembering the decision. */
        hide: () => {
            if (currentInstance) {
                currentInstance.hide();
            }
        },
        /** Opens the native install prompt if the browser offers it, otherwise shows the bar with manual instructions. */
        install: () => currentInstance ? currentInstance.install() : Promise.resolve(false),
        /** Returns true if the app runs as an installed app (standalone, fullscreen, minimal-ui or window-controls-overlay display mode). */
        isStandalone: isStandalone,
        /** Returns true if the browser offers the native install prompt. */
        canPrompt: () => !!getDeferredPrompt()
    };
}
