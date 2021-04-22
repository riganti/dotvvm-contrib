ko.bindingHandlers["dotvvm-contrib-FAIcon"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        valueAccessor().subscribe(function (previousValue) {
            const splitterIndex = previousValue.lastIndexOf("_");
            const previousIconValue = previousValue.substr(0, splitterIndex);
            const previousIconValueClass = "fa-" + previousIconValue.replace('_', '-')
            const previousStyle = previousValue.substr(splitterIndex + 1);
            const previousStyleClass = `fa${previousStyle.charAt(0)}`;
            element.classList.remove(previousStyleClass)
            element.classList.remove(previousIconValueClass);
        }, this, "beforeChange");
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        const value = ko.unwrap(valueAccessor());

        const splitterIndex = value.lastIndexOf("_");
        const iconValue = value.substr(0, splitterIndex);
        const iconValueClass = "fa-" + iconValue.replace('_', '-')
        const style = value.substr(splitterIndex + 1);
        const styleClass = `fa${style.charAt(0)}`;
        
        element.classList.add(styleClass);
        element.classList.add(iconValueClass);
    }
};

