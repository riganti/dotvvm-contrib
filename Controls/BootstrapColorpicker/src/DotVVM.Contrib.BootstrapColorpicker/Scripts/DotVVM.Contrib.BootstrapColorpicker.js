ko.bindingHandlers["dotvvm-contrib-BootstrapColorpicker"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        $(element)
            .colorpicker({
                format: "hex"
            })
            .on("colorpickerChange", function(event) {
                var property = valueAccessor();
                if (ko.isWritableObservable(property)) {
                    property(event.color.toHexString());
                }
            });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        $(element)
            .colorpicker("setValue", value);
    }
};
