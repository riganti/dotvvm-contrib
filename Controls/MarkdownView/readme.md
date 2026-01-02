# MarkdownView

Renders a Markdown string as HTML.

## Sample 1: Basic Usage

You can do this:

```DOTHTML
<dc:MarkdownView Markdown="{value: SomeMarkdown}" />
```

<br />

## Sample 2: Conditionally Enable Conversion

You can enable or disable the Markdown to HTML conversion based on a condition:

```DOTHTML
<dc:MarkdownView Markdown="{value: SomeMarkdown}" ConversionEnabled="{value: ContainsMarkdown}" />
```