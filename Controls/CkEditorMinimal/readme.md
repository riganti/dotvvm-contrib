# CkEditorMinimal

Minimalist wrapper for CkEditor. For more information visit [CKEditor documentation](https://docs.ckeditor.com/).

+  [Download official packages](https://ckeditor.com/download) from CKEditor page
+  Import the packages to your content folder eg. wwwroot
+  Register **ScriptResource** in DotvvmStartup.cs 
+  The resource name must be same like in sample (**ckeditor-config**)

```CSHARP
config.Resources.Register("ckeditor", new ScriptResource(
	    new UrlResourceLocation("~/Content/Lib/ckeditor/ckeditor.js"))
            {
                Dependencies = new[] { "jquery" }
            });

config.Resources.Register("ckeditor-config", new ScriptResource(
            new UrlResourceLocation("~/Content/Lib/ckeditor/config.js"))
            {
                Dependencies = new[] { "ckeditor"}
            });
```

## Sample 1: Usage

You can do this:

```DOTHTML
<dc:CkEditorMinimal  Html={value: HtmlText} />
```

