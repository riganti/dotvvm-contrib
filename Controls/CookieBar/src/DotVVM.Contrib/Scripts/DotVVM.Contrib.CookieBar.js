export default context => new CookieBar(context);

class CookieBar {

    constructor(context) {
        this.context = context;

        const container = context.elements[0].parentElement;
        this.popupElement = container.querySelector(".dotvvm-contrib-cookie-bar__pop-up");
        this.overlayElement = container.querySelector(".dotvvm-contrib-cookie-bar__overlay");

        if (!window.localStorage.getItem("cookieconsent")) {
            this.popupElement.classList.add("dotvvm-contrib-cookie-bar__pop-up--open");
        }

        this.checkboxes = this.overlayElement.querySelectorAll("input[type=checkbox]");
        for (const checkbox of this.checkboxes) {
            checkbox.addEventListener("click",
                function () {
                    if (this.checked) {
                        this.parentElement.classList.add("toggle-button--on");
                        this.parentElement.title = this.parentElement.dataset.titleTrue;
                    } else {
                        this.parentElement.classList.remove("toggle-button--on");
                        this.parentElement.title = this.parentElement.dataset.titleFalse;
                    }
                });
        }

        window.DotVVM = window.DotVVM || {};
        window.DotVVM.Contrib = window.DotVVM.Contrib || {};
        window.DotVVM.Contrib.CookieBar = window.DotVVM.Contrib.CookieBar ||
        {
            resetConsent: showCookieBar => {
                const consents = {};
                for (const checkbox of this.checkboxes) {
                    window.localStorage.setItem("cookieconsent__" + checkbox.parentElement.dataset.key, "denied");
                    consents[checkbox.parentElement.dataset.key] = "denied";
                }
                gtag("consent", "update", consents);
                window.localStorage.removeItem("cookieconsent");

                if (showCookieBar) {
                    this.popupElement.classList.add("dotvvm-contrib-cookie-bar__pop-up--open");
                }
            }
        };
    }

    acceptAll() {
        const consents = {};
        for (const checkbox of this.checkboxes) {
            window.localStorage.setItem("cookieconsent__" + checkbox.parentElement.dataset.key, "granted");
            consents[checkbox.parentElement.dataset.key] = "granted";
        }
        gtag("consent", "update", consents);
        window.localStorage.setItem("cookieconsent", true);

        this.hidePopup();
    }

    hidePopup() {
        this.popupElement.classList.remove("dotvvm-contrib-cookie-bar__pop-up--open");
    }

    openDialog() {
        this.hidePopup();

        this.overlayElement.style.display = "block";
        document.querySelector('html').style.overflow = "hidden";
    }

    disableAllUnnecessaryCookies() {
        for (const checkbox of this.checkboxes) {
            if (checkbox.checked) {
                checkbox.click();
            }
        }
    }

    saveAndCloseDialog() {
        var deleteCookie = (name) => {
            document.cookie = name + `=; domain=${document.domain}; expires=${new Date().toUTCString()}; path=/;`;
        };
        const consents = {};
        for (const checkbox of this.checkboxes) {
            const consentKey = checkbox.parentElement.dataset.key;
            const granted = checkbox.checked ? "granted" : "denied";
            window.localStorage.setItem("cookieconsent__" + consentKey, granted);
            consents[consentKey] = granted;
            if (consentKey == 'analytics_storage' && granted === 'denied') {
                deleteCookie('_ga');
                deleteCookie('_gid');
                deleteCookie('_gat');
            }

            else if (consentKey == 'ad_storage' && granted === 'denied') {
                deleteCookie('_gcl_au');
            }

            else if (consentKey === 'fbpixel_storage' && granted === 'denied') {
                deleteCookie('_fbp');
            }

            else if (consentKey == 'smartlook_storage' && granted === 'denied') {
                deleteCookie('SL');
            }
        }
        gtag("consent", "update", consents);

        window.localStorage.setItem("cookieconsent", true);

        this.overlayElement.style.display = "none";
        document.querySelector('html').style.overflow = "";
    }
}