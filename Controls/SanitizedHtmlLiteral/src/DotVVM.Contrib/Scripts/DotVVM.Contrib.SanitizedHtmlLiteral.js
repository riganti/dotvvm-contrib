ko.bindingHandlers["dotvvm-contrib-SanitizedHtmlLiteral"] = {
    update(element, valueAccessor) {
        const rawHtml = ko.unwrap(valueAccessor())
        const sanitizedHtml = HtmlSanitizer.SanitizeHtml(rawHtml)
        ko.virtualElements.setDomNodeChildren(element, ko.utils.parseHtmlFragment(sanitizedHtml, element.ownerDocument))
    }
};

ko.virtualElements.allowedBindings["dotvvm-contrib-SanitizedHtmlLiteral"] = true
