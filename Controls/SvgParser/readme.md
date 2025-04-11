# SvgParser

This control can render a SVG file as an inline `<svg>` element in the page. It is useful when you need to embed smaller graphics elements such as icons and do not want to force the browser to make additional requests for every SVG file.

## Sample 1: Embedding a SVG from filesystem

You can do this:

```DOTHTML
<!-- use app-relative paths -->
<dc:SvgParser Source="wwwroot/Icons/main.svg" />
```

<br />

## Sample 2: Embedding a SVG from embedded resource

You can do also this:

```DOTHTML
<dc:SvgParser Source="embedded://MyAssembly/Icons.main.svg" />
```

The format of `embedded://` URL is the same as for [embedding markup controls](https://www.dotvvm.com/docs/3.0/pages/concepts/control-development/markup-control-registration#embed-markup-control-in-a-class-library).