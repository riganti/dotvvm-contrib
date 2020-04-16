# SanitizedHtmlLiteral

The control renders the Html property running it though a HtmlSanitizer.

On server we use [mganss/HtmlSanitizer](https://github.com/mganss/HtmlSanitizer) and in Javascript we use [jitbit/HtmlSanitizer](https://github.com/jitbit/HtmlSanitizer).

## Example

When you have a `Html` property in view model, you can just show it on the page like this:

```DOTHTML
<dc:SanitizedHtmlLiteral Html={value: Html} />
```

If you don't need to refresh it on client-side, just use a `resource` binding, so we don't have to fetch the Javascript version of the sanitizer:


```DOTHTML
<dc:SanitizedHtmlLiteral Html={resource: Html} />
```
