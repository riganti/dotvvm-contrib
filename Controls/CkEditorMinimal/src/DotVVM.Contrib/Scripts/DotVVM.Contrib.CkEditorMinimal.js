(function() {
    function setupCkEditor(id, prop, element) {
        var editor = CKEDITOR.instances[id];
        if (editor) {
            editor.destroy(true);
        }

        CKEDITOR.replace(id);
        CKEDITOR.instances[id].on('change',
            function() {
                if (ko.isWritableObservable(prop.html)) {
                    element.isUpdating = true;
                    prop.html(CKEDITOR.instances[id].getData());
                    element.isUpdating = false;
                }
            });
    }

    function setCkEditorText(id, prop) {
        var htmlText = ko.isObservable(prop.html) ? prop.html() : prop.html;
        CKEDITOR.instances[id].setData(htmlText);
    }

    ko.bindingHandlers["dotvvm-contrib-CkEditorMinimal"] = {
        init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var prop = ko.unwrap(valueAccessor());
            if (!element.id) {
                element.setAttribute("id", element.attributes["data-dotvvm-id"].value);
            }

            setupCkEditor(element.id, prop, element);

            if (ko.isObservable(prop.html)) {
                prop.html.subscribe(function () {
                    if (!element.isUpdating) {
                        setCkEditorText(element.id, prop);
                    }
                });
            }
        },
        update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var prop = ko.unwrap(valueAccessor());
            if (!element.isUpdating) {
                setCkEditorText(element.id, prop);
            }
        }
    };

})();