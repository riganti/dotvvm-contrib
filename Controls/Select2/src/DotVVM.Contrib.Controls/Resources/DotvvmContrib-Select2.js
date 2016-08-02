ko.bindingHandlers["dotvvm-contrib-Select2"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // initialize select2
        $(element).select2();
        // refresh values after each postback until the control is removed from the page
        var refreshHandler = function () {
            valueAccessor().valueHasMutated();
            $(element).trigger("change");
        };
        dotvvm.events.afterPostback.subscribe(refreshHandler);
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            dotvvm.events.afterPostback.unsubscribe(refreshHandler);
            $(element).select2('destroy');
        });
    }
};
//# sourceMappingURL=DotvvmContrib-Select2.js.map