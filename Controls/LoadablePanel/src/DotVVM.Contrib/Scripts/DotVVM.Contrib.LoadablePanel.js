ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = {
    uniqueIdGenerator: (function () {
        var id = 0; 
        return function () { return id++; }; 
    })(),

    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        var panelId = element.id;
        if (!panelId) {
            panelId = ko.bindingHandlers["dotvvm-contrib-LoadablePanel"].uniqueIdGenerator();
        }

        var progressElementBinding = valueAccessor()["progressElement"];

        var progressElement = null;
        if (progressElementBinding) {
            progressElement = element.getElementsByTagName('div')[0];
        }

        var loadingItemsBinding = valueAccessor()["loadingItems"];

        function removeFromPanel() {
            if (loadingItemsBinding) {
                loadingItemsBinding.remove(panelId);
            }
        }

        function loaded() {
            element.lastChild.style.display = "";
            if (progressElement) {
                progressElement.style.display = "none";
            }
            removeFromPanel();
        }


        if (loadingItemsBinding) {
            loadingItemsBinding.push(panelId);
        }

        if (progressElement) {
            progressElement.style.display = "";
        }

        valueAccessor()["load"]().then(loaded, loaded);
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    }
};