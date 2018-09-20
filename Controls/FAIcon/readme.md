# FAIcon

Font Awesome wrapper

## Registration
For default (5.3.1 from CDN) Font Awesome icons add ```config.AddContribFAIconConfiguration()``` to DotvvmStartup.cs .
Alternatively you can provide different source for FA CSS by using ```config.AddContribFAIconConfiguration(yourFAIconResource)```.
## Sample 1: Hardcoded

```DOTHTML
    <dc:FAIcon Icon="github_brands" />
```


## Sample 2: Binding

```DOTHTML
    <dc:FAIcon Icon="{value: Icon}" />
```