# BootstrapDatepicker

Wraps [bootstrap-datepicker
](https://github.com/uxsolutions/bootstrap-datepicker) which provides a flexible datepicker widget in the Bootstrap style.

Control also contains all locales, that can be enabled during initialization.

## Initialize control

You can specify additional locales to be initialized - default locale is 'en' (English).

```csharp
// default - only en locale is available, no additional locales as initialized
config.AddContribBootstrapDatepickerConfiguration();

// selected locales - available locales are: en, cs, fr
config.AddContribBootstrapDatepickerConfiguration(new[] { "cs", "fr" });
```

## Sample 1: Basic usage

Control requires ```Date``` property - can be nullable.

```DOTHTML
<dc:BootstrapDatepicker
    Date="{value: SelectedDate}"
    />
```

<br />

## Sample 2: Language property

Control has ```Language``` property - can be set to any locale, that was initialized.

Language is automatically determined from ```CurrentUICulture```, when ```Language``` property is omitted.

```DOTHTML
<dc:BootstrapDatepicker
    Date="{value: SelectedDate}"
    Language="cs"
    />

<dc:BootstrapDatepicker
    Date="{value: SelectedDate}"
    Language="{resource: CurrentLanguage}"
    />
```

## Sample 3: Markup

input
```DOTVVM
<dc:BootstrapDatepicker Date="{value: SelectedDate}" class="form-control" />
```

component

```DOTVVM
<div class="input-group date">
    <dc:BootstrapDatepicker Date="{value: SelectedDate}" class="form-control" />
    <div class="input-group-append">
        <span class="btn btn-primary"><i class="fas fa-calendar-alt"></i></span>
    </div>
</div>
```