# CkEditorMinimal

Minimalist wrapper for CkEditor. For more information visit [CKEditor documentation](https://docs.ckeditor.com/).

If you want use our CKEditor wrapper you must [download packages](https://ckeditor.com/download) from CKEditor page and import it to your website. After import these packages you need register scripts.
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

