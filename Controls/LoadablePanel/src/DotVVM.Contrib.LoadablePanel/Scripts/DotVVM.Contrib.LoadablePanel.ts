/// <reference path="typings/dotvvm/DotVVM.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

class BindingGroup<T> {
    showProgressElement: boolean;
    loadingElementsIdsBinding: KnockoutObservableArray<KnockoutObservable<string> | string>;
    loadBinding: (abortSignal: AbortSignal) => Promise<T>;
    keyBinding: KnockoutObservable<string> | undefined ;
}

class LoadablePanelHandler {
    private static panelCounter = 0
    private static abortControllers: { [name: string]: AbortController } = {};

    init = (element: HTMLElement, valueAccessor: () => BindingGroup<any>) => {
        const bindingGroup: BindingGroup<any> = valueAccessor();

        this.subscribeToKeyChanged(bindingGroup, element);
        this.reloadPanel(bindingGroup, element);
    }

    update = (element: HTMLElement, valueAccessor: () => any) => {
    }

    loaded = (element: HTMLElement, bindingGroup: BindingGroup<any>) => {
        this.showElement(element);
        this.tryHideProgressElement(element, bindingGroup);
        this.tryRemoveFromPanel(element, bindingGroup);
    }

    private reloadPanel = (bindingGroup: BindingGroup<any>, element: HTMLElement) => {
        const panelId = this.getOrCreatePanelId(element);
        this.tryAddToPanel(panelId, bindingGroup);
        this.tryShowProgressElement(element, bindingGroup);


        const abortController = new AbortController();
        LoadablePanelHandler.abortControllers[panelId] = abortController;
        const onLoaded = () => this.loaded(element, bindingGroup);

        bindingGroup.loadBinding(abortController.signal)
            .then(onLoaded, onLoaded);
    }

    private getOrCreatePanelId = (rootElement: HTMLElement): string => {
        if (rootElement.id) {
            return rootElement.id;
        }
        LoadablePanelHandler.panelCounter++;

        const newId = `loading-panel-${LoadablePanelHandler.panelCounter}`;

        rootElement.id = newId;

        return newId;
    }

    private tryAddToPanel = (panelId: string, bindingGroup: BindingGroup<any>) => {
        if (bindingGroup.loadingElementsIdsBinding) {
            bindingGroup.loadingElementsIdsBinding.push(panelId);
        }
    }

    private tryRemoveFromPanel = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        if (!rootElement.id) {
            return;
        }
        const items = ko.unwrap(bindingGroup.loadingElementsIdsBinding);

        if (items) {
            bindingGroup.loadingElementsIdsBinding(items.filter(li => ko.unwrap(li) !== rootElement.id));
        }
    }

    private getProgressElement = (rootElement: HTMLElement): HTMLElement => {
        return rootElement.getElementsByTagName('div')[0];
    }

    private tryShowProgressElement = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        const progressElement = this.getProgressElement(rootElement);
        if (progressElement && bindingGroup.showProgressElement) {
            this.showElement(progressElement);
        }
    }

    private tryHideProgressElement = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        const progressElement = this.getProgressElement(rootElement);
        if (progressElement && bindingGroup.showProgressElement) {
            this.hideElement(progressElement);
        }
    }

    private subscribeToKeyChanged = (bindingGroup: BindingGroup<any>, element: HTMLElement) => {
        if (ko.isObservable(bindingGroup.keyBinding)) {
            bindingGroup.keyBinding.subscribe(() => {
                if (element.id && LoadablePanelHandler.abortControllers[element.id]) {
                    LoadablePanelHandler.abortControllers[element.id].abort();
                    delete LoadablePanelHandler.abortControllers[element.id];
                }
                this.reloadPanel(bindingGroup, element);
            });
        }
    }

    private showElement(element: HTMLElement) {       
        if (element.getRootNode() === window.document) {
            (element.lastElementChild as HTMLElement).style.display = "";
        }
    }

    private hideElement(element: HTMLElement) {
        if (element.getRootNode() === window.document) {
            (element.lastElementChild as HTMLElement).style.display = "none";
        }
    }
};

const inst = new LoadablePanelHandler();
ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = inst;