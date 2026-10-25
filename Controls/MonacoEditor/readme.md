# MonacoEditor

Monaco Editor control for DotVVM. It supports editing, read-only display, and side-by-side diffs.

## Setup

Install the package and register it in `DotvvmStartup.cs`:

```CSHARP
config.AddContribMonacoEditorConfiguration();
```

By default, Monaco is loaded from jsDelivr. To serve Monaco yourself, pass the URL containing its `vs` directory:

```CSHARP
config.AddContribMonacoEditorConfiguration("~/lib/monaco-editor/min");
```

## Edit mode

```DOTHTML
<dc:MonacoEditor Code="{value: SourceCode}" Language="csharp" style="height: 400px" />
```

## Read-only mode

```DOTHTML
<dc:MonacoEditor Code="{value: SourceCode}" Language="csharp" Mode="ReadOnly" style="height: 400px" />
```

## Diff mode

`Code` is the editable modified version and `OriginalCode` is displayed on the left.

```DOTHTML
<dc:MonacoEditor Code="{value: ModifiedCode}" OriginalCode="{value: OriginalCode}" Language="csharp" Mode="Diff" style="height: 400px" />
```
