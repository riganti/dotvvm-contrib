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
            _this.subscribeToKeyChanged(bindingGroup, element);
            _this.reloadPanel(bindingGroup, element);
        };
        this.update = function (element, valueAccessor) {
        };
        this.loaded = function (element, bindingGroup) {
            _this.showElement(element);
            _this.tryHideProgressElement(element, bindingGroup);
            _this.tryRemoveFromPanel(element, bindingGroup);
        };
        this.reloadPanel = function (bindingGroup, element) {
            var panelId = _this.getOrCreatePanelId(element);
            _this.tryAddToPanel(panelId, bindingGroup);
            _this.tryShowProgressElement(element, bindingGroup);
            var abortController = new AbortController();
            LoadablePanelHandler.abortControllers[panelId] = abortController;
            var onLoaded = function () { return _this.loaded(element, bindingGroup); };
            bindingGroup.loadBinding(abortController.signal)
                .then(onLoaded, onLoaded);
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
            if (bindingGroup.loadingElementsIdsBinding) {
                bindingGroup.loadingElementsIdsBinding.push(panelId);
            }
        };
        this.tryRemoveFromPanel = function (rootElement, bindingGroup) {
            if (!rootElement.id) {
                return;
            }
            var items = ko.unwrap(bindingGroup.loadingElementsIdsBinding);
            if (items) {
                bindingGroup.loadingElementsIdsBinding(items.filter(function (li) { return ko.unwrap(li) !== rootElement.id; }));
            }
        };
        this.getProgressElement = function (rootElement) {
            return rootElement.getElementsByTagName('div')[0];
        };
        this.tryShowProgressElement = function (rootElement, bindingGroup) {
            var progressElement = _this.getProgressElement(rootElement);
            if (progressElement && bindingGroup.showProgressElement) {
                _this.showElement(progressElement);
            }
        };
        this.tryHideProgressElement = function (rootElement, bindingGroup) {
            var progressElement = _this.getProgressElement(rootElement);
            if (progressElement && bindingGroup.showProgressElement) {
                _this.hideElement(progressElement);
            }
        };
        this.subscribeToKeyChanged = function (bindingGroup, element) {
            if (ko.isObservable(bindingGroup.keyBinding)) {
                bindingGroup.keyBinding.subscribe(function () {
                    if (element.id && LoadablePanelHandler.abortControllers[element.id]) {
                        LoadablePanelHandler.abortControllers[element.id].abort();
                        delete LoadablePanelHandler.abortControllers[element.id];
                    }
                    _this.reloadPanel(bindingGroup, element);
                });
            }
        };
    }
    LoadablePanelHandler.prototype.showElement = function (element) {
        if (element.getRootNode() === window.document) {
            element.lastElementChild.style.display = "";
        }
    };
    LoadablePanelHandler.prototype.hideElement = function (element) {
        if (element.getRootNode() === window.document) {
            element.lastElementChild.style.display = "none";
        }
    };
    LoadablePanelHandler.panelCounter = 0;
    LoadablePanelHandler.abortControllers = {};
    return LoadablePanelHandler;
}());
;
var inst = new LoadablePanelHandler();
ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = inst;
//# sourceMappingURL=DotVVM.Contrib.LoadablePanel.js.map