(function () {
    window.addEventListener("beforeinstallprompt", function (e) {
        // suppress the default browser mini-infobar, the InstallBar control will offer the installation instead
        e.preventDefault();
        window.__dotvvmContribInstallPrompt = e;
        window.dispatchEvent(new CustomEvent("dotvvm-contrib-install-prompt"));
    });
    window.addEventListener("appinstalled", function () {
        window.__dotvvmContribInstallPrompt = null;
        window.dispatchEvent(new CustomEvent("dotvvm-contrib-app-installed"));
    });
})();
