declare module "shared-classes" {
    export class DotvvmPostbackError {
        reason: DotvvmPostbackErrorReason;
        constructor(reason: DotvvmPostbackErrorReason);
        toString(): string;
    }
    export class CoerceError implements CoerceErrorType {
        message: string;
        path: string;
        isError: true;
        wasCoerced: false;
        get value(): never;
        constructor(message: string, path?: string);
        static generic(value: any, type: TypeDefinition): CoerceError;
        prependPathFragment(fragment: string): void;
    }
}
declare module "utils/logging" {
    type LogLevel = "normal" | "verbose";
    export const level: LogLevel;
    export function logInfoVerbose(area: string, ...args: any[]): void;
    export function logInfo(area: string, ...args: any[]): void;
    export function logWarning(area: string, ...args: any[]): void;
    export function logError(area: string, ...args: any[]): void;
    export function logPostBackScriptError(err: any): void;
}
declare module "serialization/date" {
    export function parseDate(value: string): Date | null;
    export function parseTimeSpan(value: string): number | null;
    export function parseDateTimeOffset(value: string): Date | null;
    export function serializeDate(date: string | Date | null, convertToUtc?: boolean): string | null;
    export function serializeTimeSpan(time: string | number | null): string | null;
}
declare module "utils/dom" {
    export const getElementByDotvvmId: (id: string) => HTMLElement;
    /**
     * @deprecated Use addEventListener directly
     */
    export function attachEvent(target: any, name: string, callback: (ev: PointerEvent) => any, useCapture?: boolean): void;
    export const isElementDisabled: (element: HTMLElement | null | undefined) => boolean | null | undefined;
    export function setIdFragment(idFragment: string | null | undefined): void;
}
declare module "utils/knockout" {
    export function wrapObservable<T>(obj: T): KnockoutObservable<T>;
    export function wrapObservableObjectOrArray<T>(obj: T): KnockoutObservable<T> | KnockoutObservableArray<T>;
    export function isObservableArray(target: any): boolean;
}
declare module "utils/objects" {
    export function isPrimitive(viewModel: any): boolean;
    export const createArray: <T>(a: ArrayLike<T>) => T[];
    export const hasOwnProperty: (obj: any, prop: string) => boolean;
    export const symbolOrDollar: (name: string) => symbol;
    export const keys: {
        (o: object): string[];
        (o: {}): string[];
    };
}
declare module "metadata/typeMap" {
    export function getTypeInfo(typeId: string): TypeMetadata;
    export function getObjectTypeInfo(typeId: string): ObjectTypeMetadata;
    export function getKnownTypes(): string[];
    export function updateTypeInfo(newTypes: TypeMap | undefined): void;
    export function replaceTypeInfo(newTypes: TypeMap | undefined): void;
}
declare module "serialization/deserialize" {
    export function deserialize(viewModel: any, target?: any, deserializeAll?: boolean): any;
    export function deserializePrimitive(viewModel: any, target?: any): any;
    export function deserializeDate(viewModel: any, target?: any): any;
    export function deserializeArray(viewModel: any, target?: any, deserializeAll?: boolean): any;
    export function deserializeObject(viewModel: any, target: any, deserializeAll: boolean): any;
    export function extendToObservableArrayIfRequired(observable: any): any;
}
declare module "events" {
    export class DotvvmEvent<T> {
        readonly name: string;
        private readonly triggerMissedEventsOnSubscribe;
        private handlers;
        private history;
        constructor(name: string, triggerMissedEventsOnSubscribe?: boolean);
        subscribe(handler: (data: T) => void): void;
        subscribeOnce(handler: (data: T) => void): void;
        unsubscribe(handler: (data: T) => void): void;
        trigger(data: T): void;
    }
    export const init: DotvvmEvent<DotvvmInitEventArgs>;
    export const initCompleted: DotvvmEvent<DotvvmInitCompletedEventArgs>;
    export const beforePostback: DotvvmEvent<DotvvmBeforePostBackEventArgs>;
    export const afterPostback: DotvvmEvent<DotvvmAfterPostBackEventArgs>;
    export const error: DotvvmEvent<DotvvmErrorEventArgs>;
    export const redirect: DotvvmEvent<DotvvmRedirectEventArgs>;
    export const postbackHandlersStarted: DotvvmEvent<PostbackOptions>;
    export const postbackHandlersCompleted: DotvvmEvent<PostbackOptions>;
    export const postbackResponseReceived: DotvvmEvent<DotvvmPostbackResponseReceivedEventArgs>;
    export const postbackCommitInvoked: DotvvmEvent<DotvvmPostbackCommitInvokedEventArgs>;
    export const postbackViewModelUpdated: DotvvmEvent<DotvvmPostbackViewModelUpdatedEventArgs>;
    export const postbackRejected: DotvvmEvent<DotvvmPostbackRejectedEventArgs>;
    export const staticCommandMethodInvoking: DotvvmEvent<DotvvmStaticCommandMethodInvokingEventArgs>;
    export const staticCommandMethodInvoked: DotvvmEvent<DotvvmStaticCommandMethodInvokedEventArgs>;
    export const staticCommandMethodFailed: DotvvmEvent<DotvvmStaticCommandMethodFailedEventArgs>;
    export const newState: DotvvmEvent<RootViewModel>;
}
declare module "metadata/primitiveTypes" {
    type PrimitiveTypes = {
        [name: string]: {
            tryCoerce: (value: any) => CoerceResult | undefined;
        };
    };
    export const primitiveTypes: PrimitiveTypes;
}
declare module "metadata/coercer" {
    /**
     * Validates type of value
     * @param type Expected type of type value.
     * @param originalValue Value that is known to be valid instance of type. It is used to perform incremental validation.
     */
    export function tryCoerce(value: any, type: TypeDefinition, originalValue?: any): CoerceResult;
    export function coerce(value: any, type: TypeDefinition, originalValue?: any): any;
}
declare module "postback/updater" {
    export function cleanUpdatedControls(resultObject: any, updatedControls?: any): any;
    export function restoreUpdatedControls(resultObject: any, updatedControls: any): void;
    export function updateViewModelAndControls(resultObject: any): void;
    export function patchViewModel(source: any, patch: any): any;
    export function diffViewModel(source: any, modified: any): any;
}
declare module "state-manager" {
    import { DotvvmEvent } from "events";
    export const currentStateSymbol: unique symbol;
    const notifySymbol: unique symbol;
    export const lastSetErrorSymbol: unique symbol;
    const updateSymbol: unique symbol;
    export function getIsViewModelUpdating(): boolean;
    export type UpdatableObjectExtensions<T> = {
        [notifySymbol]: (newValue: T) => void;
        [currentStateSymbol]: T;
        [updateSymbol]?: UpdateDispatcher<T>;
    };
    export class StateManager<TViewModel extends {
        $type?: TypeDefinition;
    }> {
        stateUpdateEvent: DotvvmEvent<DeepReadonly<TViewModel>>;
        readonly stateObservable: DeepKnockoutObservable<TViewModel>;
        private _state;
        get state(): DeepReadonly<TViewModel>;
        private _isDirty;
        get isDirty(): boolean;
        private _currentFrameNumber;
        constructor(initialState: DeepReadonly<TViewModel>, stateUpdateEvent: DotvvmEvent<DeepReadonly<TViewModel>>);
        dispatchUpdate(): void;
        doUpdateNow(): void;
        private startTime;
        private rerender;
        setState(newState: DeepReadonly<TViewModel>): DeepReadonly<TViewModel>;
        patchState(patch: Partial<TViewModel>): DeepReadonly<TViewModel>;
        update(updater: StateUpdate<TViewModel>): DeepReadonly<TViewModel>;
    }
    export function unmapKnockoutObservables(viewModel: any): any;
}
declare module "serialization/serialize" {
    interface ISerializationOptions {
        serializeAll?: boolean;
        ignoreSpecialProperties?: boolean;
        pathMatcher?: (vm: any) => boolean;
        path?: string[];
        pathOnly?: boolean;
        restApiTarget?: boolean;
    }
    export function serialize(viewModel: any, opt?: ISerializationOptions): any;
    export function serializeCore(viewModel: any, opt?: ISerializationOptions): any;
}
declare module "postback/resourceLoader" {
    export type RenderedResourceList = {
        [name: string]: string;
    };
    export function registerResources(rs: string[] | null | undefined): void;
    export const getRenderedResources: () => string[];
    export function loadResourceList(resources: RenderedResourceList | undefined): Promise<void>;
    export function notifyModuleLoaded(id: number): void;
}
declare module "validation/common" {
    export type DotvvmValidationContext = {
        readonly valueToValidate: any;
        readonly parentViewModel: any;
        readonly parameters: any[];
    };
    export type DotvvmValidationObservableMetadata = DotvvmValidationElementMetadata[];
    export type DotvvmValidationElementMetadata = {
        element: HTMLElement;
        dataType: string;
        format: string;
        domNodeDisposal: boolean;
        elementValidationState: boolean;
    };
    export const ErrorsPropertyName = "validationErrors";
    /** Checks if the value is null, undefined or a whitespace only string */
    export function isEmpty(value: any): boolean;
    export function getValidationMetadata(property: KnockoutObservable<any>): DotvvmValidationObservableMetadata;
}
declare module "binding-handlers/textbox-text" {
    const _default: {
        "dotvvm-textbox-text": {
            init(element: HTMLInputElement, valueAccessor: () => any, allBindingsAccessor?: KnockoutAllBindingsAccessor | undefined): void;
            update(element: HTMLInputElement, valueAccessor: () => any): void;
        };
    };
    export default _default;
}
declare module "binding-handlers/textbox-select-all-on-focus" {
    const _default_1: {
        "dotvvm-textbox-select-all-on-focus": {
            init(element: any): void;
            update(element: any, valueAccessor: () => any): void;
        };
    };
    export default _default_1;
}
declare module "binding-handlers/SSR-foreach" {
    type SeenUpdateElement = HTMLElement & {
        seenUpdate?: number;
    };
    const _default_2: {
        "dotvvm-SSR-foreach": {
            init(element: Node, valueAccessor: () => any, allBindings?: KnockoutAllBindingsAccessor | undefined, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined): {
                controlsDescendantBindings: boolean;
            };
        };
        "dotvvm-SSR-item": {
            init<T>(element: SeenUpdateElement, valueAccessor: () => T, allBindings?: any, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined): {
                controlsDescendantBindings: boolean;
            };
            update(element: SeenUpdateElement): void;
        };
    };
    export default _default_2;
}
declare module "binding-handlers/introduce-alias" {
    const _default_3: {
        'dotvvm-with-control-properties': {
            init: (element: HTMLElement, valueAccessor: () => any, allBindings?: any, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined) => {
                controlsDescendantBindings: boolean;
            };
        };
    };
    export default _default_3;
}
declare module "binding-handlers/table-columnvisible" {
    const _default_4: {
        'dotvvm-table-columnvisible': {
            init(element: HTMLElement, valueAccessor: () => any): void;
            update(element: any, valueAccessor: any): void;
        };
    };
    export default _default_4;
}
declare module "binding-handlers/enable" {
    const _default_5: {
        'dotvvm-enable': {
            update: (element: HTMLInputElement, valueAccessor: () => KnockoutObservable<boolean>) => void;
        };
    };
    export default _default_5;
}
declare module "binding-handlers/checkbox" {
    const _default_6: {
        'dotvvm-checkbox-updateAfterPostback': {
            init(element: HTMLElement, valueAccessor: () => any, allBindingsAccessor?: KnockoutAllBindingsAccessor | undefined): void;
        };
        'dotvvm-checked-pointer': {
            init(): void;
        };
        "dotvvm-CheckState": {
            init(element: HTMLElement, valueAccessor: () => any, allBindingsAccessor?: KnockoutAllBindingsAccessor | undefined, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined): void;
            update(element: any, valueAccessor: () => any): void;
        };
        "dotvvm-checkedItems": {
            after: string[] | undefined;
            init: ((element: any, valueAccessor: () => any, allBindingsAccessor?: KnockoutAllBindingsAccessor | undefined, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined) => void | {
                controlsDescendantBindings: boolean;
            }) | undefined;
            options: any;
            update(element: any, valueAccessor: () => any): void;
        };
    };
    export default _default_6;
}
declare module "postback/queue" {
    export const updateProgressChangeCounter: KnockoutObservable<number>;
    export const postbackQueues: {
        [name: string]: {
            queue: Array<(() => void)>;
            noRunning: number;
        };
    };
    export function getPostbackQueue(name?: string): {
        queue: (() => void)[];
        noRunning: number;
    };
    export function enterActivePostback(queueName: string): void;
    export function leaveActivePostback(queueName: string): void;
    export function runNextInQueue(queueName: string): void;
}
declare module "binding-handlers/update-progress" {
    const _default_7: {
        "dotvvm-UpdateProgress-Visible": {
            init(element: HTMLElement, valueAccessor: () => any, allBindingsAccessor?: KnockoutAllBindingsAccessor | undefined, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined): void;
        };
    };
    export default _default_7;
}
declare module "binding-handlers/gridviewdataset" {
    const _default_8: {
        "dotvvm-gridviewdataset": {
            init: (element: Node, valueAccessor: () => any, allBindings: KnockoutAllBindingsAccessor | undefined, _viewModel: any, bindingContext: KnockoutBindingContext | undefined) => {
                controlsDescendantBindings: boolean;
            };
        };
    };
    export default _default_8;
}
declare module "viewModules/viewModuleManager" {
    type ModuleCommand = (...args: any) => Promise<unknown>;
    export const viewModulesSymbol: unique symbol;
    export function registerViewModule(name: string, moduleObject: any): void;
    export function registerViewModules(modules: {
        [name: string]: any;
    }): void;
    export function initViewModule(name: string, viewIdOrElement: string | HTMLElement, rootElement: HTMLElement): ModuleContext;
    export function callViewModuleCommand(viewIdOrElement: string | HTMLElement, commandName: string, args: any[]): any;
    export function registerNamedCommand(viewIdOrElement: string | HTMLElement, commandName: string, command: ModuleCommand, rootElement: HTMLElement): void;
    export function unregisterNamedCommand(viewIdOrElement: string | HTMLElement, commandName: string): void;
    export class ModuleContext {
        readonly moduleName: string;
        readonly elements: HTMLElement[];
        readonly properties: {
            [name: string]: any;
        };
        private readonly namedCommands;
        module: any;
        constructor(moduleName: string, elements: HTMLElement[], properties: {
            [name: string]: any;
        });
        registerNamedCommand: (name: string, command: (...args: any[]) => Promise<any>) => void;
        unregisterNamedCommand: (name: string) => void;
    }
}
declare module "binding-handlers/with-view-modules" {
    const _default_9: {
        'dotvvm-with-view-modules': {
            init: (element: HTMLElement, valueAccessor: () => any, allBindings?: any, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined) => {
                controlsDescendantBindings: boolean;
            };
        };
    };
    export default _default_9;
}
declare module "binding-handlers/named-command" {
    const _default_10: {
        'dotvvm-named-command': {
            init: (element: HTMLElement, valueAccessor: () => any, allBindings?: any, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined) => {
                controlsDescendantBindings: boolean;
            };
        };
    };
    export default _default_10;
}
declare module "binding-handlers/file-upload" {
    const _default_11: {
        "dotvvm-FileUpload": {
            init: (element: HTMLInputElement, valueAccessor: () => any, allBindings?: any, viewModel?: any, bindingContext?: KnockoutBindingContext | undefined) => void;
        };
    };
    export default _default_11;
}
declare module "binding-handlers/all-handlers" {
    type KnockoutHandlerDictionary = {
        [name: string]: KnockoutBindingHandler;
    };
    const allHandlers: KnockoutHandlerDictionary;
    export default allHandlers;
}
declare module "spa/events" {
    import { DotvvmEvent } from "events";
    export const spaNavigating: DotvvmEvent<DotvvmSpaNavigatingEventArgs>;
    export const spaNavigated: DotvvmEvent<DotvvmSpaNavigatedEventArgs>;
    export const spaNavigationFailed: DotvvmEvent<DotvvmSpaNavigationFailedEventArgs>;
}
declare module "dotvvm-base" {
    import { StateManager } from "state-manager";
    export function getViewModel(): DeepKnockoutObservableObject<RootViewModel>;
    export function getViewModelCacheId(): string | undefined;
    export function getViewModelCache(): any;
    export function getViewModelObservable(): DeepKnockoutObservable<RootViewModel>;
    export function getInitialUrl(): string;
    export function getVirtualDirectory(): string;
    export function replaceViewModel(vm: RootViewModel): void;
    export function getState(): Readonly<RootViewModel>;
    export function updateViewModelCache(viewModelCacheId: string, viewModelCache: any): void;
    export function clearViewModelCache(): void;
    export function getCulture(): string;
    export function getStateManager(): StateManager<RootViewModel>;
    export function initCore(culture: string): void;
    export function initBindings(): void;
}
declare module "DotVVM.Globalize" {
    import { parseDate as serializationParseDate } from "serialization/date";
    export function format(format: string, ...values: any[]): string;
    type GlobalizeFormattable = null | undefined | string | Date | number;
    export function formatString(format: string | null | undefined, value: GlobalizeFormattable | KnockoutObservable<GlobalizeFormattable>): string;
    export function parseNumber(value: string): number;
    export function parseDate(value: string, format: string, previousValue?: Date): Date | null;
    export const parseDotvvmDate: typeof serializationParseDate;
    export function bindingDateToString(value: KnockoutObservable<string | Date> | string | Date, format?: string): "" | KnockoutComputed<string>;
    export function bindingNumberToString(value: KnockoutObservable<string | number> | string | number, format?: string): "" | KnockoutComputed<string>;
}
declare module "DotVVM.Polyfills" {
    export default function (): void;
}
declare var compileConstants: {
    /** If the compiled bundle is for SPA applications */
    isSpa: boolean;
    /** If the compiled bundle is for legacy browser that don't support modules (and other new EcmaScript features)  */
    nomodules: boolean;
};
declare module "utils/uri" {
    export function removeVirtualDirectoryFromUrl(url: string): string;
    export function addVirtualDirectoryToUrl(appRelativeUrl: string): string;
    export function addLeadingSlash(url: string): string;
    export function concatUrl(url1: string, url2: string): string;
}
declare module "postback/http" {
    export type WrappedResponse<T> = {
        readonly result: T;
        readonly response?: Response;
    };
    export function getJSON<T>(url: string, spaPlaceHolderUniqueId?: string, signal?: AbortSignal, additionalHeaders?: {
        [key: string]: string;
    }): Promise<WrappedResponse<T>>;
    export function postJSON<T>(url: string, postData: any, signal: AbortSignal | undefined, additionalHeaders?: {
        [key: string]: string;
    }): Promise<WrappedResponse<T>>;
    export function fetchJson<T>(url: string, init: RequestInit): Promise<WrappedResponse<T>>;
    export function fetchCsrfToken(signal: AbortSignal | undefined): Promise<string>;
    export function retryOnInvalidCsrfToken<TResult>(postbackFunction: () => Promise<TResult>, iteration?: number, customErrorHandler?: () => void): Promise<TResult>;
}
declare module "postback/counter" {
    export function backUpPostBackCounter(): number;
}
declare module "utils/magic-navigator" {
    export function navigate(url: string): void;
}
declare module "postback/redirect" {
    export function performRedirect(url: string, replace: boolean, allowSpa: boolean): Promise<any>;
    export function handleRedirect(options: PostbackOptions, resultObject: any, response: Response, replace?: boolean): Promise<DotvvmRedirectEventArgs>;
}
declare module "utils/evaluator" {
    export function evaluateOnViewModel(context: any, expression: string): any;
    export function getDataSourceItems(viewModel: any): Array<KnockoutObservable<any>>;
    export function wrapObservable(func: () => any, isArray?: boolean): KnockoutComputed<any>;
    export const unwrapComputedProperty: (obs: any) => any;
}
declare module "postback/gate" {
    export function isPostbackDisabled(postbackId: number): boolean;
    export function enablePostbacks(): void;
    export function disablePostbacks(): void;
}
declare module "validation/validators" {
    import { DotvvmValidationContext } from "validation/common";
    export type DotvvmValidator = {
        isValid: (value: any, context: DotvvmValidationContext, property: KnockoutObservable<any>) => boolean;
    };
    export const required: DotvvmValidator;
    export const regex: DotvvmValidator;
    export const intRange: DotvvmValidator;
    export const enforceClientFormat: DotvvmValidator;
    export const range: DotvvmValidator;
    export const notNull: DotvvmValidator;
    export const emailAddress: DotvvmValidator;
    type DotvvmValidatorSet = {
        [name: string]: DotvvmValidator;
    };
    export const validators: DotvvmValidatorSet;
}
declare module "validation/error" {
    export const allErrors: ValidationError[];
    export function detachAllErrors(): void;
    export function getErrors<T>(o: KnockoutObservable<T> | null): ValidationError[];
    export class ValidationError {
        errorMessage: string;
        validatedObservable: KnockoutObservable<any>;
        private constructor();
        static attach(errorMessage: string, observable: KnockoutObservable<any>): ValidationError;
        detach(): void;
    }
}
declare module "postback/internal-handlers" {
    export const isPostbackRunning: KnockoutObservable<boolean>;
    export const suppressOnDisabledElementHandler: DotvvmPostbackHandler;
    export const isPostBackRunningHandler: DotvvmPostbackHandler;
    export const concurrencyDefault: (o: any) => {
        name: string;
        before: string[];
        execute: (next: () => Promise<PostbackCommitFunction>, options: PostbackOptions) => Promise<PostbackCommitFunction>;
    };
    export const concurrencyDeny: (o: any) => {
        name: string;
        before: string[];
        execute(next: () => Promise<PostbackCommitFunction>, options: PostbackOptions): Promise<PostbackCommitFunction>;
    };
    export const concurrencyQueue: (o: any) => {
        name: string;
        before: string[];
        execute(next: () => Promise<PostbackCommitFunction>, options: PostbackOptions): Promise<PostbackCommitFunction>;
    };
    export const suppressOnUpdating: (o: any) => {
        name: string;
        before: string[];
        execute(next: () => Promise<PostbackCommitFunction>, options: PostbackOptions): Promise<PostbackCommitFunction>;
    };
    export function isPostbackStillActive(options: PostbackOptions): boolean;
}
declare module "postback/handlers" {
    class ConfirmPostBackHandler implements DotvvmPostbackHandler {
        message: string;
        constructor(message: string);
        execute<T>(next: () => Promise<T>, options: PostbackOptions): Promise<T>;
    }
    class SuppressPostBackHandler implements DotvvmPostbackHandler {
        suppress: boolean;
        constructor(suppress: boolean);
        execute<T>(next: () => Promise<T>, options: PostbackOptions): Promise<T>;
    }
    export const confirm: (options: any) => ConfirmPostBackHandler;
    export const suppress: (options: any) => SuppressPostBackHandler;
    export const timeout: (options: any) => DotvvmPostbackHandler;
    export const postbackHandlers: DotvvmPostbackHandlerCollection;
    export function getPostbackHandler(name: string): (options: any) => DotvvmPostbackHandler;
    export const defaultConcurrencyPostbackHandler: DotvvmPostbackHandler;
}
declare module "validation/actions" {
    type DotvvmValidationActions = {
        [name: string]: (element: HTMLElement, errorMessages: string[], param: any) => void;
    };
    export const elementActions: DotvvmValidationActions;
}
declare module "validation/validation" {
    import { ValidationError } from "validation/error";
    import { DotvvmEvent } from "events";
    type DotvvmValidationErrorsChangedEventArgs = PostbackOptions & {
        readonly allErrors: ValidationError[];
    };
    export const events: {
        validationErrorsChanged: DotvvmEvent<DotvvmValidationErrorsChangedEventArgs>;
    };
    export const globalValidationObject: {
        rules: {
            [name: string]: import("validation/validators").DotvvmValidator;
        };
        errors: ValidationError[];
        events: {
            validationErrorsChanged: DotvvmEvent<DotvvmValidationErrorsChangedEventArgs>;
        };
    };
    export function init(): void;
    /**
     * Adds validation errors from the server to the appropriate arrays
     */
    export function showValidationErrorsFromServer(dataContext: any, path: string, serverResponseObject: any, options: PostbackOptions): void;
}
declare module "postback/postbackCore" {
    export function throwIfAborted(options: PostbackOptions): void;
    export function getLastStartedPostbackId(): number;
    export function postbackCore(options: PostbackOptions, path: string[], command: string, controlUniqueId: string, context: any, commandArgs?: any[]): Promise<PostbackCommitFunction>;
}
declare module "postback/postback" {
    export function postBack(sender: HTMLElement, path: string[], command: string, controlUniqueId: string, context?: any, handlers?: ClientFriendlyPostbackHandlerConfiguration[], commandArgs?: any[], abortSignal?: AbortSignal): Promise<DotvvmAfterPostBackEventArgs>;
    type MaybePromise<T> = Promise<T> | T;
    export function applyPostbackHandlers(next: (options: PostbackOptions) => MaybePromise<PostbackCommitFunction | any>, sender: HTMLElement, handlerConfigurations?: ClientFriendlyPostbackHandlerConfiguration[], args?: any[], context?: any, viewModel?: any, abortSignal?: AbortSignal): Promise<DotvvmAfterPostBackEventArgs>;
    export function isPostbackHandler(obj: any): obj is DotvvmPostbackHandler;
    export function sortHandlers(handlers: DotvvmPostbackHandler[]): DotvvmPostbackHandler[];
}
declare module "spa/navigation" {
    export function navigateCore(url: string, options: PostbackOptions, handlePageNavigating: (url: string) => void): Promise<DotvvmNavigationEventArgs>;
}
declare module "spa/spa" {
    export const isSpaReady: KnockoutObservable<boolean>;
    export function init(): void;
    export function getSpaPlaceHoldersUniqueId(): string;
    export function handleSpaNavigation(element: HTMLElement): Promise<DotvvmNavigationEventArgs | undefined>;
    export function handleSpaNavigationCore(url: string | null, sender?: HTMLElement, handlePageNavigating?: (url: string) => void): Promise<DotvvmNavigationEventArgs>;
}
declare module "binding-handlers/register" {
    const _default_12: () => void;
    export default _default_12;
}
declare module "postback/staticCommand" {
    export function staticCommandPostback(sender: HTMLElement, command: string, args: any[], options: PostbackOptions): Promise<any>;
}
declare module "controls/routeLink" {
    export function buildRouteUrl(routePath: string, params: any): string;
    export function buildUrlSuffix(urlSuffix: string, query: any): string;
}
declare module "controls/fileUpload" {
    export function showUploadDialog(sender: HTMLElement): void;
    export function reportProgress(inputControl: HTMLInputElement, isBusy: boolean, progress: number, result: DotvvmStaticCommandResponse<DotvvmFileUploadData[]> | string): void;
}
declare module "api/eventHub" {
    export function notify(id: string): void;
    export function get(id: string): KnockoutObservable<number>;
}
declare module "api/api" {
    type ApiComputed<T> = KnockoutObservable<T | null> & {
        refreshValue: (throwOnError?: boolean) => PromiseLike<any> | undefined;
    };
    export function invoke<T>(target: any, methodName: string, argsProvider: () => any[], refreshTriggers: (args: any[]) => Array<KnockoutObservable<any> | string>, notifyTriggers: (args: any[]) => string[], element: HTMLElement, sharingKeyProvider: (args: any[]) => string[]): ApiComputed<T>;
    export function refreshOn<T>(value: ApiComputed<T>, watch: KnockoutObservable<any>): ApiComputed<T>;
}
declare module "metadata/metadataHelper" {
    export function getTypeId(viewModel: object): string | undefined;
    export function getTypeMetadata(typeId: string): TypeMetadata;
    export function getEnumMetadata(enumMetadataId: string): EnumTypeMetadata;
    export function getEnumValue(identifier: string | number, enumMetadataId: string): number | undefined;
}
declare module "collections/sortingHelper" {
    type ElementType = string | number | boolean;
    export const orderBy: <T>(array: T[], selector: (item: T) => ElementType, typeId: string) => T[];
    export const orderByDesc: <T>(array: T[], selector: (item: T) => ElementType, typeId: string) => T[];
}
declare module "collections/arrayHelper" {
    import { orderBy, orderByDesc } from "collections/sortingHelper";
    export { add, addOrUpdate, addRange, clear, distinct, firstOrDefault, insert, insertRange, lastOrDefault, max, min, orderBy, orderByDesc, removeAll, removeAt, removeFirst, removeLast, removeRange, reverse, setItem };
    function add<T>(observable: any, element: T): void;
    function addOrUpdate<T>(observable: any, element: T, matcher: (e: T) => boolean, updater: (e: T) => T): void;
    function addRange<T>(observable: any, elements: T[]): void;
    function clear(observable: any): void;
    function distinct<T>(array: T[]): T[];
    function firstOrDefault<T>(array: T[], predicate: (s: T) => boolean): T | null;
    function insert<T>(observable: any, index: number, element: T): void;
    function insertRange<T>(observable: any, index: number, elements: T[]): void;
    function lastOrDefault<T>(array: T[], predicate: (s: T) => boolean): T | null;
    function max<T>(array: T[], selector: (item: T) => number, throwIfEmpty: boolean): number | null;
    function min<T>(array: T[], selector: (item: T) => number, throwIfEmpty: boolean): number | null;
    function removeAt<T>(observable: any, index: number): void;
    function removeAll<T>(observable: any, predicate: (s: T) => boolean): void;
    function removeRange<T>(observable: any, index: number, length: number): void;
    function removeFirst<T>(observable: any, predicate: (s: T) => boolean): void;
    function removeLast<T>(observable: any, predicate: (s: T) => boolean): void;
    function reverse<T>(observable: any): void;
    function setItem<T>(observable: any, index: number, value: T): void;
}
declare module "collections/dictionaryHelper" {
    type Dictionary<Key, Value> = {
        Key: Key;
        Value: Value;
    }[];
    export function clear(observable: any): void;
    export function containsKey<Key, Value>(dictionary: Dictionary<Key, Value>, identifier: Key): boolean;
    export function getItem<Key, Value>(dictionary: Dictionary<Key, Value>, identifier: Key): Value;
    export function remove<Key, Value>(observable: any, identifier: Key): boolean;
    export function setItem<Key, Value>(observable: any, identifier: Key, value: Value): void;
}
declare module "utils/stringHelper" {
    export function split<T>(text: string, delimiter: string, options: string): string[];
    export function join<T>(elements: T[], delimiter: string): string;
    export function format(pattern: string, expressions: any[]): string;
}
declare module "dotvvm-root" {
    import { getCulture } from "dotvvm-base";
    import * as events from "events";
    import { postBack } from "postback/postback";
    import { serialize } from "serialization/serialize";
    import { serializeDate, parseDate } from "serialization/date";
    import { deserialize } from "serialization/deserialize";
    import * as evaluator from "utils/evaluator";
    import * as globalize from "DotVVM.Globalize";
    import { staticCommandPostback } from "postback/staticCommand";
    import { applyPostbackHandlers } from "postback/postback";
    import { isSpaReady } from "spa/spa";
    import { buildRouteUrl, buildUrlSuffix } from "controls/routeLink";
    import * as fileUpload from "controls/fileUpload";
    import { handleSpaNavigation } from "spa/spa";
    import * as spaEvents from "spa/events";
    import * as api from "api/api";
    import * as eventHub from "api/eventHub";
    import * as viewModuleManager from "viewModules/viewModuleManager";
    import { notifyModuleLoaded } from "postback/resourceLoader";
    import { logError, logWarning, logInfo, logInfoVerbose, logPostBackScriptError } from "utils/logging";
    import * as metadataHelper from "metadata/metadataHelper";
    function init(culture: string): void;
    const dotvvmExports: {
        getCulture: typeof getCulture;
        evaluator: {
            getDataSourceItems: typeof evaluator.getDataSourceItems;
            wrapObservable: typeof evaluator.wrapObservable;
        };
        fileUpload: {
            reportProgress: typeof fileUpload.reportProgress;
            showUploadDialog: typeof fileUpload.showUploadDialog;
        };
        api: {
            invoke: typeof api.invoke;
            refreshOn: typeof api.refreshOn;
        };
        eventHub: {
            get: typeof eventHub.get;
            notify: typeof eventHub.notify;
        };
        globalize: typeof globalize;
        postBackHandlers: DotvvmPostbackHandlerCollection;
        postbackHandlers: DotvvmPostbackHandlerCollection;
        buildUrlSuffix: typeof buildUrlSuffix;
        buildRouteUrl: typeof buildRouteUrl;
        staticCommandPostback: typeof staticCommandPostback;
        applyPostbackHandlers: typeof applyPostbackHandlers;
        validation: {
            rules: {
                [name: string]: import("validation/validators").DotvvmValidator;
            };
            errors: import("validation/error").ValidationError[];
            events: {
                validationErrorsChanged: events.DotvvmEvent<PostbackOptions & {
                    readonly allErrors: import("validation/error").ValidationError[];
                }>;
            };
        };
        postBack: typeof postBack;
        init: typeof init;
        isPostbackRunning: KnockoutObservable<boolean>;
        events: Partial<typeof spaEvents> & typeof events;
        viewModels: {
            root: {
                readonly viewModel: DeepKnockoutObservableObject<RootViewModel>;
            };
        };
        readonly state: Readonly<RootViewModel>;
        patchState(a: any): void;
        viewModelObservables: {
            readonly root: KnockoutObservable<DeepKnockoutObservableObject<RootViewModel>>;
        };
        serialization: {
            serialize: typeof serialize;
            serializeDate: typeof serializeDate;
            parseDate: typeof parseDate;
            deserialize: typeof deserialize;
        };
        metadata: {
            getTypeId: typeof metadataHelper.getTypeId;
            getTypeMetadata: typeof metadataHelper.getTypeMetadata;
            getEnumMetadata: typeof metadataHelper.getEnumMetadata;
            getEnumValue: typeof metadataHelper.getEnumValue;
        };
        viewModules: {
            registerOne: typeof viewModuleManager.registerViewModule;
            init: typeof viewModuleManager.initViewModule;
            call: typeof viewModuleManager.callViewModuleCommand;
            registerMany: typeof viewModuleManager.registerViewModules;
        };
        resourceLoader: {
            notifyModuleLoaded: typeof notifyModuleLoaded;
        };
        log: {
            logError: typeof logError;
            logWarning: typeof logWarning;
            logInfo: typeof logInfo;
            logInfoVerbose: typeof logInfoVerbose;
            logPostBackScriptError: typeof logPostBackScriptError;
            level: "normal" | "verbose";
        };
        translations: any;
    };
    global {
        const dotvvm: typeof dotvvmExports & {
            isSpaReady?: typeof isSpaReady;
            handleSpaNavigation?: typeof handleSpaNavigation;
        };
        interface Window {
            dotvvm: typeof dotvvmExports;
        }
    }
    export default dotvvmExports;
}
declare type PostbackCommitFunction = (...args: any) => Promise<DotvvmAfterPostBackEventArgs>;
declare type DotvvmPostbackHandler = {
    execute(next: () => Promise<PostbackCommitFunction>, options: PostbackOptions): Promise<PostbackCommitFunction>;
    name?: string;
    after?: Array<string | DotvvmPostbackHandler>;
    before?: Array<string | DotvvmPostbackHandler>;
};
declare type DotvvmPostbackErrorLike = {
    readonly reason: DotvvmPostbackErrorReason;
};
declare type DotvvmPostbackErrorReason = {
    type: 'handler';
    handlerName: string;
    message?: string;
} | {
    type: 'network';
    err?: any;
} | {
    type: 'gate';
} | {
    type: 'commit';
    args?: DotvvmErrorEventArgs;
} | {
    type: 'csrfToken';
} | {
    type: 'serverError';
    status?: number;
    responseObject: any;
    response?: Response;
} | {
    type: 'event';
} | {
    type: 'validation';
    responseObject: any;
    response?: Response;
} | {
    type: 'abort';
} & {
    options?: PostbackOptions;
};
declare type PostbackCommandType = "postback" | "staticCommand" | "spaNavigation";
declare type PostbackOptions = {
    readonly postbackId: number;
    readonly commandType: PostbackCommandType;
    readonly args: any[];
    readonly sender?: HTMLElement;
    readonly viewModel?: any;
    serverResponseObject?: any;
    validationTargetPath?: string;
    abortSignal?: AbortSignal;
};
declare type DotvvmErrorEventArgs = PostbackOptions & {
    readonly response?: Response;
    readonly error: DotvvmPostbackErrorLike;
    handled: boolean;
};
declare type DotvvmBeforePostBackEventArgs = PostbackOptions & {
    cancel: boolean;
};
declare type DotvvmAfterPostBackEventArgs = PostbackOptions & {
    /** Set to true in case the postback did not finish and it was cancelled by an event or a postback handler */
    readonly wasInterrupted?: boolean;
    readonly commandResult?: any;
    readonly response?: Response;
    readonly error?: DotvvmPostbackErrorLike;
};
declare type DotvvmNavigationEventArgs = PostbackOptions & {
    readonly url: string;
};
declare type DotvvmSpaNavigatingEventArgs = DotvvmNavigationEventArgs & {
    cancel: boolean;
};
declare type DotvvmSpaNavigatedEventArgs = DotvvmNavigationEventArgs & {
    readonly response?: Response;
};
declare type DotvvmSpaNavigationFailedEventArgs = DotvvmNavigationEventArgs & {
    readonly response?: Response;
    readonly error?: DotvvmPostbackErrorLike;
};
declare type DotvvmRedirectEventArgs = DotvvmNavigationEventArgs & {
    readonly response?: Response;
    /** Whether the new url should replace the current url in the browsing history */
    readonly replace: boolean;
};
declare type DotvvmPostbackHandlersStartedEventArgs = PostbackOptions & {};
declare type DotvvmPostbackHandlersCompletedEventArgs = PostbackOptions & {};
declare type DotvvmPostbackResponseReceivedEventArgs = PostbackOptions & {
    readonly response: Response;
};
declare type DotvvmPostbackCommitInvokedEventArgs = PostbackOptions & {
    readonly response: Response;
};
declare type DotvvmPostbackViewModelUpdatedEventArgs = PostbackOptions & {
    readonly response: Response;
};
declare type DotvvmPostbackRejectedEventArgs = PostbackOptions & {
    readonly error: DotvvmPostbackErrorLike;
};
declare type DotvvmStaticCommandMethodEventArgs = PostbackOptions & {
    readonly methodId: string;
    readonly methodArgs: any[];
};
declare type DotvvmStaticCommandMethodInvokingEventArgs = DotvvmStaticCommandMethodEventArgs & {};
declare type DotvvmStaticCommandMethodInvokedEventArgs = DotvvmStaticCommandMethodEventArgs & {
    readonly result: any;
    readonly response?: Response;
};
declare type DotvvmStaticCommandMethodFailedEventArgs = DotvvmStaticCommandMethodEventArgs & {
    readonly result?: any;
    readonly response?: Response;
    readonly error: DotvvmPostbackErrorLike;
};
declare type DotvvmInitEventArgs = {
    readonly viewModel: any;
};
declare type DotvvmInitCompletedEventArgs = {};
interface DotvvmViewModelInfo {
    viewModel?: any;
    viewModelCacheId?: string;
    viewModelCache?: any;
    renderedResources?: string[];
    url?: string;
    virtualDirectory?: string;
    typeMetadata: TypeMap;
}
interface DotvvmViewModels {
    [name: string]: DotvvmViewModelInfo;
    root: DotvvmViewModelInfo;
}
interface DotvvmPostbackHandlerCollection {
    [name: string]: ((options: any) => DotvvmPostbackHandler);
}
declare type DotvvmStaticCommandResponse<T = any> = {
    result: any;
    customData: {
        [key: string]: any;
    };
    typeMetadata?: TypeMap;
} | {
    action: "redirect";
    url: string;
    replace?: boolean;
    allowSpa?: boolean;
};
declare type DotvvmPostBackHandlerConfiguration = {
    name: string;
    options: (context: KnockoutBindingContext) => any;
};
declare type ClientFriendlyPostbackHandlerConfiguration = string | DotvvmPostbackHandler | DotvvmPostBackHandlerConfiguration | [string, object] | [string, (context: KnockoutBindingContext, data: any) => any];
declare type PropertyValidationRuleInfo = {
    ruleName: string;
    errorMessage: string;
    parameters: any[];
};
declare type ValidationRuleTable = {
    [type: string]: {
        [property: string]: [PropertyValidationRuleInfo];
    };
};
declare type StateUpdate<TViewModel> = (initial: DeepReadonly<TViewModel>) => DeepReadonly<TViewModel>;
declare type UpdateDispatcher<TViewModel> = (update: StateUpdate<TViewModel>) => void;
/** Knockout observable, including all child object and arrays */
declare type DeepKnockoutObservable<T> = T extends (infer R)[] ? DeepKnockoutObservableArray<R> : T extends object ? KnockoutObservable<DeepKnockoutObservableObject<T>> : KnockoutObservable<T>;
declare type DeepKnockoutObservableArray<T> = KnockoutObservableArray<DeepKnockoutObservable<T>>;
declare type DeepKnockoutObservableObject<T> = {
    readonly [P in keyof T]: DeepKnockoutObservable<T[P]>;
};
/** Partial<T>, but including all child objects  */
declare type DeepPartial<T> = T extends object ? {
    [P in keyof T]?: DeepPartial<T[P]>;
} : T;
/** Readonly<T>, but including all child objects and arrays  */
declare type DeepReadonly<T> = T extends TypeDefinition ? T : T extends (infer R)[] ? readonly DeepReadonly<R>[] : T extends object ? {
    readonly [P in keyof T]: DeepReadonly<T[P]>;
} : T;
/** Knockout observable that is found in the DotVVM ViewModel - all nested objects and arrays are also observable + it has some helper functions (state, patchState, ...) */
declare type DotvvmObservable<T> = DeepKnockoutObservable<T> & {
    /** A property, returns latest state from dotvvm.state. It does not contain any knockout observable and does not have any propagation delay, as the value in the observable */
    readonly state: DeepReadonly<T>;
    /** Sets new state directly into the dotvvm.state.
     * Note that the value arrives into the observable itself asynchronously, so there might be slight delay */
    readonly setState: (newState: DeepReadonly<T>) => void;
    /** Patches the current state and sets it into dotvvm.state.
     * Compared to setState, when property does not exist in the patch parameter, the old value from state is used.
     * Note that the value arrives into the observable itself asynchronously, so there might be slight delay
     * @example observable.patchState({ Prop2: 0 }) // Only must be specified, although Prop1 also exists and is required  */
    readonly patchState: (patch: DeepReadonly<DeepPartial<T>>) => void;
    /** Dispatches update of the state.
     * Note that the value arrives into the observable itself asynchronously, so there might be slight delay
     * @example observable.updater(state => [ ...state, newElement ]) // This appends an element to an (observable) array
     * @example observable.updater(state => state + 1) // Increments the value by one
     * @example observable.updater(state => ({ ...state, MyProperty: state.MyProperty + 1 })) // Increments the property MyProperty by one
     */
    readonly updater: UpdateDispatcher<T>;
};
declare type RootViewModel = {
    $type: string;
    $csrfToken?: string;
    [name: string]: any;
};
declare type TypeMap = {
    [typeId: string]: TypeMetadata;
};
declare type ObjectTypeMetadata = {
    type: "object";
    properties: {
        [prop: string]: PropertyMetadata;
    };
};
declare type EnumTypeMetadata = {
    type: "enum";
    values: {
        [name: string]: number;
    };
    isFlags?: boolean;
};
declare type TypeMetadata = ObjectTypeMetadata | EnumTypeMetadata;
declare type PropertyMetadata = {
    type: TypeDefinition;
    post?: "always" | "pathOnly" | "no";
    update?: "always" | "firstRequest" | "no";
    validationRules?: PropertyValidationRuleInfo[];
    clientExtenders?: ClientExtenderInfo[];
};
declare type TypeDefinition = string | {
    readonly type: "nullable";
    readonly inner: TypeDefinition;
} | {
    readonly type: "dynamic";
} | TypeDefinition[];
declare type ClientExtenderInfo = {
    name: string;
    parameter: any;
};
declare type CoerceErrorType = {
    isError: true;
    wasCoerced: false;
    message: string;
    path: string;
    prependPathFragment(fragment: string): void;
    value: never;
};
declare type CoerceResult = CoerceErrorType | {
    value: any;
    wasCoerced?: boolean;
    isError?: false;
};
declare type DotvvmFileUploadCollection = {
    Files: KnockoutObservableArray<KnockoutObservable<DotvvmFileUploadData>>;
    Progress: KnockoutObservable<number>;
    Error: KnockoutObservable<string>;
    IsBusy: KnockoutObservable<boolean>;
};
declare type DotvvmFileUploadData = {
    FileId: KnockoutObservable<string>;
    FileName: KnockoutObservable<string>;
    FileSize: KnockoutObservable<DotvvmFileSize>;
    IsFileTypeAllowed: KnockoutObservable<boolean>;
    IsMaxSizeExceeded: KnockoutObservable<boolean>;
    IsAllowed: KnockoutObservable<boolean>;
};
declare type DotvvmFileSize = {
    Bytes: KnockoutObservable<number>;
    FormattedText: KnockoutObservable<string>;
};
