ko.bindingHandlers["dotvvm-contrib-FAIcon"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        valueAccessor().subscribe(function (previousValue) {
            if (previousValue.endsWith("brands")) {
                element.classList.remove("fab");
            } else {
                element.classList.remove("fas");
            }
            previousValue = previousValue.substr(0, previousValue.lastIndexOf("_"));
            element.classList.remove("fa-" + previousValue.replace('_', '-'));
        }, this, "beforeChange");
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        if (value.endsWith("brands")) {
            element.classList.add("fab");
        } else {
            element.classList.add("fas");
        }

        value = value.substr(0, value.lastIndexOf("_"));
        element.classList.add("fa-" + value.replace('_', '-'));
    }
};

