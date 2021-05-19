ko.bindingHandlers["dotvvm-contrib-FAIcon"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        removePreviousClasses(element);
        const value = ko.unwrap(valueAccessor());
        addClassesBasedOnIcon(element, value);

        function removePreviousClasses(element) {
            const classListArray = [...element.classList];
            const previousFontAwesomeClasses = classListArray.filter(function (value) {
                return value.startsWith("fa");
            })
            element.classList.remove(...previousFontAwesomeClasses);
        }

        function addClassesBasedOnIcon(element, value) {
            const splitterIndex = value.lastIndexOf("_");
            const iconValue = value.substr(0, splitterIndex);
            const iconClass = "fa-" + iconValue.replace('_', '-')
            const style = value.substr(splitterIndex + 1);
            const styleClass = `fa${style.charAt(0)}`;

            element.classList.add(styleClass);
            element.classList.add(iconClass);
        }
    },
};