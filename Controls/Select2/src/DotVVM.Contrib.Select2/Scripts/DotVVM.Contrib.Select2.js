ko.bindingHandlers["dotvvm-contrib-Select2"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var prop = valueAccessor();
        // initialize select2
        $(element).select2({
            'allowClear': prop.AllowClear,
            'placeholder': prop.Placeholder
        });

        // remove the handler when the control is removed from the page
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).select2('destroy');
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // Access the value binding to trigger update function when value is updated
        ko.unwrap(valueAccessor());

        // Trigger change event for select2 to update currently selected values, but do not trigger onchange event 
        // directly on the select element
        function updateSelectedValues() {
            var onchangeEventHandler = element.getAttribute("onchange");
            element.setAttribute("onchange", "");
            $(element).trigger("change.select2");
            element.setAttribute("onchange", onchangeEventHandler);
        }

        window.setTimeout(updateSelectedValues, 0);
    }
};
