export default context => new CookieBar(context);

class CookieBar {

    constructor(context) {
        this.context = context;

        const container = context.elements[0].parentElement;
        this.popupElement = container.querySelector(".dotvvm-contrib-cookie-bar__pop-up");
        this.dialogElement = container.querySelector("#consentDialog");
        this.checkboxes = this.dialogElement.querySelectorAll("input[type=checkbox]");

        if (!window.localStorage.getItem("cookieconsent")) {
            this.popupElement.classList.add("dotvvm-contrib-cookie-bar__pop-up--open");
        } else {
            const consents = {};
            for (const checkbox of this.checkboxes) {
                const consentKey = checkbox.parentElement.dataset.key;
                const granted = window.localStorage.getItem("cookieconsent__" + consentKey) || "denied";
                consents[consentKey] = granted;
            }
            gtag("consent", "update", consents);
        }

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

        this.dialogElement.addEventListener('cancel', () => {
            document.querySelector('html').style.overflow = "initial";
        });
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

        this.dialogElement.showModal();
        document.querySelector('html').style.overflow = "hidden";
    }

    disableAllUnnecessaryCookies() {
        for (const checkbox of this.checkboxes) {
            if (checkbox.checked) {
                checkbox.click();
            }
        }
    }

    disableAllUnnecessaryCookiesAndSave() {
        this.disableAllUnnecessaryCookies();
        this.save();
        this.hidePopup();
    }

    deleteCookie(nameRegex) {
        const cookieNames = document.cookie
            .split(";")
            .map(x => x.trim())
            .map(x => x.slice(0, -x.split("=").pop().length - 1))
            .filter(x => x.match(nameRegex));

        for (const name of cookieNames) {
            let domain = document.domain;
            const domainParts = domain.split(".");
            for (const part of domainParts) {
                domain = domain.slice(part.length + 1);
                document.cookie = name + `=; domain=${domain}; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
            }

            document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        }
    }

    saveAndCloseDialog() {
        this.save();

        this.dialogElement.close();
        document.querySelector('html').style.overflow = "";
    }

    save() {
        const consents = {};
        for (const checkbox of this.checkboxes) {
            const consentKey = checkbox.parentElement.dataset.key;
            const granted = checkbox.checked ? "granted" : "denied";
            window.localStorage.setItem("cookieconsent__" + consentKey, granted);
            consents[consentKey] = granted;

            const cookieNameRegexes = checkbox.parentElement.dataset.cookieNameRegexes;
            if (cookieNameRegexes && granted === "denied") {
                for (let name of cookieNameRegexes.split(",")) {
                    this.deleteCookie(name);
                }
            }
        }
        gtag("consent", "update", consents);

        window.localStorage.setItem("cookieconsent", true);
    }
}