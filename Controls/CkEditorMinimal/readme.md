# CkEditorMinimal

Minimalist wrapper for CkEditor. For more information visit [CKEditor documentation](https://docs.ckeditor.com/)

If you want use our CKEditor wrapper you must [download packages](https://ckeditor.com/download) from CKEditor page and import it to your website. After import these packages you need register scripts
<script>
config.Resources.Register("ckeditor", new ScriptResource(new UrlResourceLocation("~/Content/Lib/ckeditor/ckeditor.js"))
            {
                Dependencies = new[] { "jquery" }
            });
config.Resources.Register("ckeditor-scripts", new ScriptResource(new UrlResourceLocation("~/Content/Lib/ckeditor/config.js"))
            {
                Dependencies = new[] { "ckeditor"}

            });
</script>

We have only wrapper for input setting. 

## Sample 1: Something

You can do this:

```DOTHTML
<dc:CkEditorMinimal  Html={value: HtmlText} />

<dot:RequiredResource Name="ckeditor-scripts" />
```

