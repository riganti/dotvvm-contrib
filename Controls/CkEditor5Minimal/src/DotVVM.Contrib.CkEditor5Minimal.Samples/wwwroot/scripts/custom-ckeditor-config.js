export function createEditor(element, defaults, configName) {
    if (configName === "compact") {
        defaults.toolbar.items = ["undo", "redo", "|", "bold", "italic", "link"];
        defaults.placeholder = "Use the compact editor configuration.";
    }

    if (configName === "extended") {
        defaults.toolbar.items = [
            "undo", "redo", "|", "heading", "|",
            "bold", "italic", "underline", "|",
            "link", "insertImage", "insertTable", "|",
            "bulletedList", "numberedList", "|", "sourceEditing"
        ];
        defaults.placeholder = "Use the extended editor configuration.";
    }

    return window.CKEDITOR.ClassicEditor.create(element, defaults);
}
