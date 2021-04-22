# FAIcon

Font Awesome wrapper

## Registration
For default (5.3.1 from CDN) Font Awesome icons add ```config.AddContribFAIconConfiguration()``` to DotvvmStartup.cs .

If you want to use Pro icons (eg icons with light style) you have to include FA CSS by using ```config.AddContribFAIconProConfiguration(yourFAIconResource)```.

## Sample 1: Hardcoded

```DOTHTML
    <dc:FAIcon Icon="github_brands" />
```


## Sample 2: Binding

```DOTHTML
    <dc:FAIcon Icon="{value: Icon}" />
```