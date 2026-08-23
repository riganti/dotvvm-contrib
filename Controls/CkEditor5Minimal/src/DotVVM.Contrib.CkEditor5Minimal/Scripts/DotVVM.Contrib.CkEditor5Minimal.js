(function () {
    "use strict";

    var configModuleImports = {};
    var updatingElement = null;

    var defaultConfig = {
        licenseKey: "GPL",
        toolbar: {
            items: [
                "undo", "redo", "|", "heading", "|",
                "bold", "italic", "underline", "subscript", "superscript", "code", "removeFormat", "|",
                "link", "insertImage", "insertTable", "|",
                "codeBlock", "blockQuote", "bulletedList", "numberedList", "|", "sourceEditing"
            ],
            shouldNotGroupWhenFull: false
        },
        heading: {
            options: [
                { model: "paragraph", title: "Paragraph", class: "" },
                { model: "heading1", view: "h1", title: "Heading 1", class: "" },
                { model: "heading2", view: "h2", title: "Heading 2", class: "" },
                { model: "heading3", view: "h3", title: "Heading 3", class: "" },
                { model: "heading4", view: "h4", title: "Heading 4", class: "" }
            ]
        },
        image: {
            toolbar: [
                "imageStyle:inline", "imageStyle:block", "imageStyle:side", "|",
                "imageTextAlternative", "|", "linkImage"
            ],
            insert: { integrations: ["url"] },
            resizeOptions: [
                { name: "resizeImage:original", value: null, label: "Original" },
                { name: "resizeImage:custom", value: "custom", label: "Custom" },
                { name: "resizeImage:40", value: "40", label: "40%" },
                { name: "resizeImage:60", value: "60", label: "60%" }
            ]
        },
        link: {
            addTargetToExternalLinks: true,
            defaultProtocol: "https://"
        },
        table: {
            contentToolbar: ["tableColumn", "tableRow", "mergeTableCells"]
        },
        codeBlock: {
            languages: [
                { language: "plaintext", label: "Plain text" },
                { language: "cs", label: "C#" },
                { language: "css", label: "CSS" },
                { language: "diff", label: "Diff" },
                { language: "dothtml", label: "DotHTML" },
                { language: "html", label: "HTML" },
                { language: "javascript", label: "JavaScript" },
                { language: "python", label: "Python" },
                { language: "typescript", label: "TypeScript" },
                { language: "xml", label: "XML" }
            ]
        },
        placeholder: "Type or paste your content here!",
        htmlSupport: {
            allow: [
                { name: "address" },
                { name: "blockquote" },
                { name: "figure", classes: ["table"] },
                { name: "table" },
                { name: "thead" },
                { name: "tbody" },
                { name: "tr" },
                { name: "th", attributes: ["colspan", "rowspan", "scope"] },
                { name: "td", attributes: ["colspan", "rowspan"] }
            ]
        }
    };

    function getCkEditor() {
        if (!window.CKEDITOR || !window.CKEDITOR.ClassicEditor) {
            throw new Error("CKEditor 5 is not loaded.");
        }
        return window.CKEDITOR;
    }

    function disableNestedTables(editor) {
        editor.model.schema.addChildCheck(function (context, childDefinition) {
            if (childDefinition.name === "table" && Array.from(context.getNames()).indexOf("table") !== -1) {
                return false;
            }
        });
    }

    function createDefaultConfig() {
        var ckeditor = getCkEditor();
        var config = JSON.parse(JSON.stringify(defaultConfig));
        config.plugins = [
            ckeditor.AccessibilityHelp,
            ckeditor.Autoformat,
            ckeditor.Autosave,
            ckeditor.Bold,
            ckeditor.BlockQuote,
            ckeditor.Code,
            ckeditor.CodeBlock,
            ckeditor.Essentials,
            ckeditor.Heading,
            ckeditor.Image,
            ckeditor.ImageStyle,
            ckeditor.ImageResize,
            ckeditor.LinkImage,
            ckeditor.ImageToolbar,
            ckeditor.ImageInsert,
            ckeditor.ImageInsertViaUrl,
            ckeditor.Italic,
            ckeditor.Link,
            ckeditor.List,
            ckeditor.Paragraph,
            ckeditor.PasteFromOffice,
            ckeditor.RemoveFormat,
            ckeditor.SelectAll,
            ckeditor.SourceEditing,
            ckeditor.Subscript,
            ckeditor.Superscript,
            ckeditor.Table,
            ckeditor.TableToolbar,
            ckeditor.Underline,
            ckeditor.Undo,
            ckeditor.GeneralHtmlSupport
        ];
        config.extraPlugins = [disableNestedTables];
        return config;
    }

    function importConfigModule(url) {
        if (!configModuleImports[url]) {
            configModuleImports[url] = import(url);
        }
        return configModuleImports[url];
    }

    function createDefaultEditor(element) {
        return getCkEditor().ClassicEditor.create(element, createDefaultConfig());
    }

    function updateValue(element, value, newValue) {
        updatingElement = element;
        try {
            value(newValue === undefined ? element.ckEditorInstance.getData() : newValue);
        } finally {
            updatingElement = null;
        }
    }

    ko.bindingHandlers["dotvvm-contrib-CkEditor5Minimal"] = {
        init: function (element, valueAccessor) {
            var props = ko.unwrap(valueAccessor());
            var configModuleUrl = element.getAttribute("data-config-module-url");

            (configModuleUrl ? importConfigModule(configModuleUrl) : Promise.resolve(null)).then(function (module) {
                if (module && typeof module.createEditor !== "function") {
                    throw new Error("CKEditor 5 configuration module '" + configModuleUrl + "' must export createEditor.");
                }
                return module
                    ? module.createEditor(element, createDefaultConfig(), ko.unwrap(props.configName))
                    : createDefaultEditor(element);
            }).then(function (editor) {
                element.ckEditorInstance = editor;
                var initialValue = ko.unwrap(props.html) || "";
                editor.setData(initialValue);

                if (ko.isWriteableObservable(props.html)) {
                    props.html.subscribe(function (newValue) {
                        if (updatingElement !== element) {
                            editor.setData(newValue || "");
                        }
                    });
                    editor.model.document.on("change:data", function () {
                        updateValue(element, props.html);
                    });

                    if (editor.plugins.has("SourceEditing")) {
                        var sourceEditing = editor.plugins.get("SourceEditing");
                        sourceEditing.on("change:isSourceEditingMode", function (event, name, isSourceEditingMode) {
                            if (!isSourceEditingMode) {
                                return;
                            }

                            var editorRoot = editor.editing.view.getDomRoot();
                            var sourceEditingTextarea = editorRoot && editorRoot.nextSibling && editorRoot.nextSibling.firstChild;
                            if (sourceEditingTextarea instanceof HTMLTextAreaElement) {
                                sourceEditingTextarea.addEventListener("blur", function () {
                                    updateValue(element, props.html, sourceEditingTextarea.value);
                                });
                            }
                        });
                    }
                }

                if (editor.getData() !== initialValue && ko.isWriteableObservable(props.html)) {
                    updateValue(element, props.html);
                }

                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    editor.destroy();
                });
            });
        }
    };
})();
