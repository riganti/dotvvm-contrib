# HumanizeDateTime

Wraps the [Humanizer](https://github.com/Humanizr/Humanizer) library (.NET) and [Day.js](https://day.js.org/) (JavaScript) to display `DateTime` values as human-readable relative time strings (e.g. _"2 hours ago"_, _"in 3 days"_).

The control renders the humanized value on the server side and also keeps it up to date on the client side. It supports localization via the DotVVM culture setting.

## Installation

Register the library in `DotvvmStartup.cs`:

```csharp
config.AddContribHumanizerConfiguration();
```

## Sample 1: Hard-coded value

```DOTHTML
<dc:HumanizeDateTime Value="2020-01-01 10:00:00" />
```

## Sample 2: Bound value

```DOTHTML
<dc:HumanizeDateTime Value="{value: LastModified}" />
```

## Sample 3: AutoUpdate

The `AutoUpdate` property recalculates the humanized text every minute on the client side without a server postback:

```DOTHTML
<dc:HumanizeDateTime Value="{value: LastModified}" AutoUpdate="true" />
```

## Sample 4: Humanize() in value bindings

After calling `config.AddContribHumanizerConfiguration()`, the `Humanize()` extension method can be used directly in DotVVM value bindings. It is automatically translated to client-side JavaScript using Day.js:

```DOTHTML
<span>{{value: LastModified.Humanize()}}</span>
```
