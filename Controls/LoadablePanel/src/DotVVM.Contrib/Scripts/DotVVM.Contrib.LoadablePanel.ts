/// <reference path="typings/dotvvm/DotVVM.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

class BindingGroup<T>{
    progressElement: boolean
    loadingItems: KnockoutObservableArray<KnockoutObservable<string>| string>
    load: () => Promise<T>
}

class LoadablePanelHandler {
    private static panelCounter = 0 

    init = (element: HTMLElement, valueAccessor: () => BindingGroup<any>) => {
        const bindingGroup: BindingGroup<any> = valueAccessor();

        const context = ko.contextFor(element).$data as KnockoutObservable<any>;

        context.subscribe(() => console.info("Aaaa"))

        this.reloadPanel(bindingGroup, element);
    }

    update = (element: HTMLElement, valueAccessor: () => any) => {
    }

    loaded = (element: HTMLElement, bindingGroup: BindingGroup<any>) => {
        (element.lastElementChild as HTMLElement).style.display = "";

        this.tryHideProgressElement(element, bindingGroup);
        this.tryRemoveFromPanel(element, bindingGroup);
    }

    private reloadPanel = (bindingGroup: BindingGroup<any>, element: HTMLElement) => {
        const panelId = this.getOrCreatePanelId(element);
        this.tryAddToPanel(panelId, bindingGroup);
        this.tryShowProgressElement(element, bindingGroup);

        const onLoaded = () => this.loaded(element, bindingGroup);

        bindingGroup.load().then(onLoaded, onLoaded);
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
        if (bindingGroup.loadingItems) {
            bindingGroup.loadingItems.push(panelId);
        }
    }

    private tryRemoveFromPanel = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        if (!rootElement.id) {
            return;
        }
        const items = ko.unwrap(bindingGroup.loadingItems);

        if (items) {
            bindingGroup.loadingItems(items.filter(li => ko.unwrap(li) !== rootElement.id));
        }
    }

    private getProgressElement = (rootElement: HTMLElement): HTMLElement => {
        return rootElement.getElementsByTagName('div')[0];
    }

    private tryShowProgressElement = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        const progressElement = this.getProgressElement(rootElement);
        if (progressElement && bindingGroup.progressElement) {
            progressElement.style.display = "";
        }
    }

    private tryHideProgressElement = (rootElement: HTMLElement, bindingGroup: BindingGroup<any>): void => {
        const progressElement = this.getProgressElement(rootElement);
        if (progressElement && bindingGroup.progressElement) {
            progressElement.style.display = "none";
        }
    }
};

const inst = new LoadablePanelHandler();
ko.bindingHandlers["dotvvm-contrib-LoadablePanel"] = inst;