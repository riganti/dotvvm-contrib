ko.bindingHandlers["dotvvm-contrib-Select2"] = {
    init(element: any, valueAccessor: () => any, allBindingsAccessor: KnockoutAllBindingsAccessor, viewModel: any, bindingContext: KnockoutBindingContext) {

        // initialize select2
        $(element).select2();

        // refresh values after each postback until the control is removed from the page
        var refreshHandler = () => {
            valueAccessor().valueHasMutated();
            $(element).trigger("change");
        };
        dotvvm.events.afterPostback.subscribe(refreshHandler);
        ko.utils.domNodeDisposal.addDisposeCallback(element, () => {
            dotvvm.events.afterPostback.unsubscribe(refreshHandler);    
        });
    }
};