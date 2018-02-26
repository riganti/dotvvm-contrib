ko.bindingHandlers["dotvvm-contrib-Select2"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        // initialize select2
        $(element).select2();

        // refresh values after each postback until the control is removed from the page
        var refreshHandler = function () {
            dotvvm.isViewModelUpdating = true;
            valueAccessor().valueHasMutated();
        //    $(element).trigger("change.select2");
            dotvvm.isViewModelUpdating = false;
        };

        // watch for the changes on the server
        dotvvm.events.afterPostback.subscribe(refreshHandler);

        // remove the handler when the control is removed from the page
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            dotvvm.events.afterPostback.unsubscribe(refreshHandler);
            $(element).select2('destroy');
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        // TODO: update the control with a new value from the viewmodel
    }
};
