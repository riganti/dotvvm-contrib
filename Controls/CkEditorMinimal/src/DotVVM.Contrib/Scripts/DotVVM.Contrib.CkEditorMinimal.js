
function setupCkEditor(id) {
    var editor = CKEDITOR.instances[id];
    if (editor) {
        editor.destroy(true);
    }
    CKEDITOR.replace(id);
    CKEDITOR.add
}

function changeCkEditor(id, prop) {
    CKEDITOR.instances[id].on('change', function () { prop.html(CKEDITOR.instances[id].getData()); });
}

ko.bindingHandlers["dotvvm-contrib-CkEditorMinimal"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var prop = valueAccessor();
        var elementId = element.id;

        setupCkEditor(elementId);
        changeCkEditor(elementId, prop);
    }
};
