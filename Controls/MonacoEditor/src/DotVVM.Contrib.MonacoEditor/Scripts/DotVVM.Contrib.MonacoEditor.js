ko.bindingHandlers["dotvvm-contrib-MonacoEditor"] = {
    init: function (element, valueAccessor) {
        var props = valueAccessor();
        var isDisposed = false;

        if (props.mode === "Diff" && !props.originalCode) {
            throw new Error("OriginalCode is required when MonacoEditor uses Diff mode.");
        }

        var monacoBaseUrl = getMonacoBaseUrl();
        window.MonacoEnvironment = window.MonacoEnvironment || {};
        if (!window.MonacoEnvironment.getWorkerUrl) {
            window.MonacoEnvironment.getWorkerUrl = function () {
                var workerScript = "importScripts('" + monacoBaseUrl + "/base/worker/workerMain.js');";
                return "data:text/javascript;charset=utf-8," + encodeURIComponent(workerScript);
            };
        }

        window.require.config({
            paths: {
                vs: monacoBaseUrl
            }
        });

        window.require(["vs/editor/editor.main"], function (monaco) {
            if (isDisposed) {
                return;
            }

            var editor;
            var model;
            var originalModel;
            var modifiedModel;
            var isModelUpdate = false;
            var subscriptions = [];

            if (props.mode === "Diff") {
                originalModel = monaco.editor.createModel(ko.unwrap(props.originalCode), props.language);
                modifiedModel = monaco.editor.createModel(ko.unwrap(props.code), props.language);
                model = modifiedModel;
                editor = monaco.editor.createDiffEditor(element);
                editor.setModel({
                    original: originalModel,
                    modified: modifiedModel
                });

                subscriptions.push(props.originalCode.subscribe(function (value) {
                    setModelValue(originalModel, value);
                }));
            } else {
                model = monaco.editor.createModel(ko.unwrap(props.code), props.language);
                editor = monaco.editor.create(element, {
                    model: model,
                    readOnly: props.mode === "ReadOnly"
                });
            }

            if (props.mode !== "ReadOnly") {
                model.onDidChangeContent(function () {
                    if (!isModelUpdate && ko.isWriteableObservable(props.code)) {
                        props.code(model.getValue());
                    }
                });
            }

            subscriptions.push(props.code.subscribe(function (value) {
                setModelValue(model, value);
            }));

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                isDisposed = true;
                subscriptions.forEach(function (subscription) {
                    subscription.dispose();
                });
                editor.dispose();
                model.dispose();
                if (originalModel) {
                    originalModel.dispose();
                }
            });

            function setModelValue(targetModel, value) {
                if (targetModel.getValue() === value) {
                    return;
                }

                isModelUpdate = true;
                try {
                    targetModel.setValue(value);
                } finally {
                    isModelUpdate = false;
                }
            }
        });
    }
};

function getMonacoBaseUrl() {
    var loaderUrl = Array.prototype.find.call(
        document.scripts,
        function (script) {
            return /\/vs\/loader\.js(?:\?|$)/.test(script.src);
        });

    if (!loaderUrl) {
        throw new Error("The Monaco AMD loader was not found.");
    }

    return loaderUrl.src.replace(/\/vs\/loader\.js(?:\?.*)?$/, "/vs");
}
