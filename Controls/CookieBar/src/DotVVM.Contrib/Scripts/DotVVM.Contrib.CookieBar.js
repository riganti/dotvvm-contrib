export default context => new CookieBar(context);

class CookieBar {

    constructor(context) {
        this.context = context;

        const container = context.elements[0].parentElement;
        this.popupElement = container.querySelector(".cookies__pop-up");
        this.overlayElement = container.querySelector(".cookies__overlay");

        if (!window.localStorage.getItem("cookieconsent")) {
            this.popupElement.classList.add("cookies__pop-up--open");
        } else {
            for (const rule of this.context.properties.Rules()) {
                const ruleValue = rule();
                const granted = window.localStorage.getItem("cookieconsent__" + ruleValue.Key()) === "granted";
                ruleValue.Enabled(granted);
            }
        }

        this.checkboxes = this.overlayElement.querySelectorAll("input[type=checkbox]");
        for (const checkbox of this.checkboxes) {
            checkbox.addEventListener("click",
                function() {
                    if (this.checked) {
                        this.parentElement.classList.add("toggle-button--on");
                        this.parentElement.title = this.parentElement.dataset.titleTrue;
                    } else {
                        this.parentElement.classList.remove("toggle-button--on");
                        this.parentElement.title = this.parentElement.dataset.titleFalse;
                    }
                });
        }
    }

    acceptAll() {
        const consents = {};
        for (const rule of this.context.properties.Rules()) {
            const ruleValue = rule();
            window.localStorage.setItem("cookieconsent__" + ruleValue.Key(), "granted");
            consents[ruleValue.Key()] = "granted";
        }
        gtag("consent", "update", consents);
        window.localStorage.setItem("cookieconsent", true);

        this.hidePopup();
    }

    hidePopup() {
        this.popupElement.classList.remove("cookies__pop-up--open");
    }

    openDialog() {
        this.hidePopup();

        this.overlayElement.style.display = "block";
        document.body.style.overflow = "hidden";
    }

    saveAndCloseDialog() {
        const consents = {};
        for (const checkbox of this.checkboxes) {
            const granted = checkbox.checked ? "granted" : "denied";
            window.localStorage.setItem("cookieconsent__" + checkbox.parentElement.dataset.key, granted);
            consents[checkbox.parentElement.dataset.key] = granted;
        }
        gtag("consent", "update", consents);

        window.localStorage.setItem("cookieconsent", true);

        this.overlayElement.style.display = "none";
        document.body.style.overflow = "";
    }

}