# CkEditor5Minimal

A minimal DotVVM wrapper for CKEditor 5. It supplies the editor configuration used by Riganti CMS, except for CMS-specific extensions such as its asset picker.
The default configuration uses CKEditor 5 under the GPL license.

Register the control in `DotvvmStartup.cs`. It registers CKEditor 5 version 43.3.1 and its required stylesheet from jsDelivr as dependencies.

```csharp
config.AddContribCkEditor5MinimalConfiguration();
```

Use the control with a value binding:

```DOTHTML
<dc:CkEditor5Minimal Html="{value: HtmlText}" />
```

## Custom CKEditor configuration

Register a configuration ES module as a `ScriptModuleResource`, then set `ConfigResourceName`. The control verifies that the named resource is a `ScriptModuleResource`, imports it directly, and calls its `createEditor(element, defaults, configName)` export. The optional `ConfigName` lets one module supply multiple editor configurations on the same page.

```csharp
config.Resources.Register("my-ckeditor-config", new ScriptModuleResource(
    new UrlResourceLocation("~/scripts/my-ckeditor-config.js"))
{
    Dependencies = new[] { CkEditor5MinimalConfigurationExtensions.CkEditorResourceName }
});
```

```dothtml
<dc:CkEditor5Minimal Html="{value: HtmlText}"
                     ConfigResourceName="my-ckeditor-config"
                     ConfigName="compact" />
```

```javascript
export function createEditor(element, defaults, configName) {
    // Replace GPL with your CKEditor commercial license key when applicable.
    defaults.licenseKey = "your-commercial-license-key";

    if (configName === "compact") {
        defaults.toolbar.items = ["undo", "redo", "|", "bold", "italic"];
    }

    return window.CKEDITOR.ClassicEditor.create(element, defaults);
}
```