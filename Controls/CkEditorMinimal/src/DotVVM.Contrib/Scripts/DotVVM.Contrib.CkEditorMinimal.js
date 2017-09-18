
function setupCkEditor(id, prop) {
    var editor = CKEDITOR.instances[id];
    var htmlText = ko.isObservable(prop.html) ? prop.html() : prop.html;

    if (editor) {
        editor.destroy(true);
    }

    CKEDITOR.replace(id);
    CKEDITOR.instances[id].setData(htmlText);
    CKEDITOR.instances[id].on('change', function () { prop.html(CKEDITOR.instances[id].getData()); });
}

ko.bindingHandlers["dotvvm-contrib-CkEditorMinimal"] = { 
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var prop = ko.unwrap(valueAccessor());
        if (!element.id){
            element.setAttribute("id", element.attributes["data-dotvvm-id"].value);
        }

        setupCkEditor(element.id, prop);
    }
};
