/// <reference path="typings/dotvvm/DotVVM.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
var BindingGroup = /** @class */ (function () {
    function BindingGroup() {
    }
    return BindingGroup;
}());
var LoadablePanelHandler = /** @class */ (function () {
    function LoadablePanelHandler() {
        var _this = this;
        this.init = function (element, valueAccessor) {
            var bindingGroup = valueAccessor();
            var context = ko.contextFor(element).$data;
            context.subscribe(function () { return console.info("Aaaa"); });
            _this.reloadPanel(bindingGroup, element);
        };
        this.update = function (element, valueAccessor) {
        };
        this.loaded = function (element, bindingGroup) {
            element.lastElementChild.style.display = "";
            _this.tryHideProgressElement(element, bindingGroup);
            _this.tryRemoveFromPanel(element, bindingGroup);
        };
        this.reloadPanel = function (bindingGroup, element) {
            var panelId = _this.getOrCreatePanelId(element);
            _this.tryAddToPanel(panelId, bindingGroup);
            _this.tryShowProgressElement(element, bindingGroup);
            var onLoaded = function () { return _this.loaded(element, bindingGroup); };
            bindingGroup.load().then(onLoaded, onLoaded);
        };
        this.getOrCreatePanelId = function (rootElement) {
            if (rootElement.id) {
                return rootElement.id;
            }
            LoadablePanelHandler.panelCounter++;
            var newId = "loading-panel-" + LoadablePanelHandler.panelCounter;
            rootElement.id = newId;
            return newId;
        };
        this.tryAddToPanel = function (panelId, bindingGroup) {
            if (bindingGroup.loadingItems) {
                bindingGroup.loadingItems.push(panelId);
            }
        };
        this.tryRemoveFromPanel = function (rootElement, bindingGroup) {
            if (!rootElement.id) {
                return;
            }
            var items = ko.unwrap(bindingGroup.loadingItems);
            if (items) {
                bindingGroup.loadingItems(items.filter(function (li) { return ko.unwrap(li) !== rootElement.id; }));
            }
        };
        this.getProgressElement = function (rootElement) {
            return rootElement.getElementsByTagName('div')[0];
        };
        this.tryShowProgressElement = function (rootElement, bindingGroup) {
            var progressElement = _this.getProgressElement(rootElement);
            if (progressElement && bindingGroup.progressElement) {
                progressElement.style.display = "";
            }
        };
        this.tryHideProgressElement = function (rootElement, bindingGroup) {
            var progressElement = _this.getProgressElement(rootElement);
            if (progressElement && bindingGroup.progressElement) {
                progressElement.style.display = "none";
            }
        };
    }
    LoadablePanelHandler.panelCounter = 0;
    return LoadablePanelHandler;
}());
;
var inst = new LoadablePanelHandler();
ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = inst;
//# sourceMappingURL=DotVVM.Contrib.LoadablePanel.js.map