# Welcome to the DotVVM-contrib!

This is an official repository of community controls for [DotVVM](https://github.com/riganti/dotvvm). 
Feel free to send us pull requests with your contributions.

The controls are published on NuGet regularly.

## How To Use Them

1. Run the `Install-Package DotVVM.Contrib.XYZ` in your project.

2. In the `DotvvmStartup.cs` file, add the following line to register the controls:

```
config.AddContribXYZConfiguration();
```

<br />

## List of Controls

* [BootstrapColorpicker](Controls/BootstrapColorpicker/readme.md)
* [BootstrapDatepicker](Controls/BootstrapDatepicker/readme.md)
* [CkEditorMinimal](Controls/CkEditorMinimal/readme.md)
* [CookieBar](Controls/CookieBar/readme.md)
* [EditableForm](Controls/EditableForm/readme.md)
* [FAIcon](Controls/FAIcon/readme.md)
* [GoogleAnalyticsJavascript](Controls/GoogleAnalyticsJavascript/readme.md)
* [GoogleMap](Controls/GoogleMap/readme.md)
* [HeroIcon](Controls/HeroIcon/readme.md)
* [LoadablePanel](Controls/LoadablePanel/readme.md)
* [MultilevelMenu](Controls/MultilevelMenu/readme.md)
* [NoUiSlider](Controls/NoUiSlider/readme.md)
* [PolicyView](Controls/PolicyView/readme.md)
* [PolymorphTemplateSelector](Controls/PolymorphTemplateSelector/readme.md)
* [QrCode](Controls/QrCode/readme.md)
* [Select2](Controls/Select2/readme.md)
* [SvgParser](Controls/SvgParser/readme.md)
* [TemplateSelector](Controls/TemplateSelector/readme.md)
* [TypeAhead](Controls/TypeAhead/readme.md)

<br />

## How To Create Controls

1. Clone the repository and open the command prompt in the root directory of the cloned repository.

2. Install "dotnet new" template to start building the controls.

```
dotnet new -i ./Controls/_template
``` 

3. Create a new directory for the control in the `Controls` directory and run the template (don't forget to substitute `XYZ` with the proper name of the control):

```
cd Controls
mkdir XYZ
cd XYZ
dotnet new dotvvm-contrib -n XYZ
```

4. Open the solution and implement the control, sample app and Selenium tests.
