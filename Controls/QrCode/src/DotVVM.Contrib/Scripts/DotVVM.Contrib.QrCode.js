ko.bindingHandlers["dotvvm-contrib-QrCode"] = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        while (element.firstChild) {
            element.removeChild(element.firstChild);
        }
        const value = valueAccessor();
        if (value) {
            jQuery(element).qrcode(ko.unwrap(value["content"]));
        }
    }
};
