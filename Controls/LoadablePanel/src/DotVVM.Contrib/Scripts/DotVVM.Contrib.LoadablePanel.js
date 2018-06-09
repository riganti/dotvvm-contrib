ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var progressElementBinding = valueAccessor()["progressElement"];

        var progressElement = null;
        if (progressElementBinding) {
            progressElement = progressElementBinding;
        }

        function loaded() {
            element.lastChild.style.display = "";
            if (progressElement) {
                progressElement.style.display = "none";
            }
        }

        if (progressElement) {
            progressElement.style.display = "";
        }
        valueAccessor()["load"]().then(loaded, loaded);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    }
};