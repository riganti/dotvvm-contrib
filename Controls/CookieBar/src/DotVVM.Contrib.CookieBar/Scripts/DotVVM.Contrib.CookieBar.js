export default context => new CookieBar(context);

class CookieBar {

    constructor(context) {
        this.context = context;

        const container = context.elements[0].parentElement;
        this.popupElement = container.querySelector(".dotvvm-contrib-cookie-bar__pop-up");
        this.dialogElement = container.querySelector("#consentDialog");
        this.checkboxes = this.dialogElement.querySelectorAll("input[type=checkbox]");

        let hasSavedConsent = false;
        const consents = {};
        try {
            hasSavedConsent = !!window.localStorage.getItem("cookieconsent");
            for (const checkbox of this.checkboxes) {
                const key = checkbox.parentElement.dataset.key;
                consents[key] = hasSavedConsent && window.localStorage.getItem("cookieconsent__" + key) === "granted"
                    ? "granted" : "denied";
            }
        } catch (error) {
            console.warn("CookieBar: saved consent could not be read; optional consent remains denied.", error);
            hasSavedConsent = false;
            for (const checkbox of this.checkboxes) {
                consents[checkbox.parentElement.dataset.key] = "denied";
            }
        }
        for (const checkbox of this.checkboxes) {
            this.setCheckbox(checkbox, consents[checkbox.parentElement.dataset.key] === "granted");
        }

        if (!hasSavedConsent) {
            this.popupElement.classList.add("dotvvm-contrib-cookie-bar__pop-up--open");
        } else {
            gtag("consent", "update", consents);
        }

        for (const checkbox of this.checkboxes) {
            checkbox.addEventListener("click",
                () => this.setCheckbox(checkbox, checkbox.checked));
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
                    this.setCheckbox(checkbox, false);
                }
                gtag("consent", "update", consents);
                window.localStorage.removeItem("cookieconsent");

                if (showCookieBar) {
                    this.popupElement.classList.add("dotvvm-contrib-cookie-bar__pop-up--open");
                }
            }
        };

        this.dialogElement.addEventListener('cancel', () => {
            this.setDocumentOverflow('initial');
        });
    }

    acceptAll() {
        for (const checkbox of this.checkboxes) {
            this.setCheckbox(checkbox, true);
        }
        this.save();

        this.hidePopup();
    }

    hidePopup() {
        this.popupElement.classList.remove("dotvvm-contrib-cookie-bar__pop-up--open");
    }

    openDialog() {
        for (const checkbox of this.checkboxes) {
            const granted = window.localStorage.getItem("cookieconsent") &&
                window.localStorage.getItem("cookieconsent__" + checkbox.parentElement.dataset.key) === "granted";
            this.setCheckbox(checkbox, !!granted);
        }
        this.hidePopup();

        this.dialogElement.showModal();
        this.setDocumentOverflow('hidden');
    }

    disableAllUnnecessaryCookies() {
        for (const checkbox of this.checkboxes) {
            this.setCheckbox(checkbox, false);
        }
    }

    setCheckbox(checkbox, granted) {
        checkbox.checked = granted;
        checkbox.parentElement.classList.toggle("toggle-button--on", granted);
        checkbox.parentElement.title = granted
            ? checkbox.parentElement.dataset.titleTrue
            : checkbox.parentElement.dataset.titleFalse;
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
        this.setDocumentOverflow('initial');
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

    setDocumentOverflow(string) {
        document.querySelector('html').style.overflow = string;
    }
}