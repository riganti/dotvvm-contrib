# Welcome to the DotVVM-contrib!

This is an official repository for community extensions and controls. Feel free to send us pull requests with your contributions.

<br />

## List of Controls

* [Select2](Controls/Select2/readme.md)


<br />

## Control Development Guidelines

+ Each control should has its own directory in the `Controls` directory (e.g. `Controls/Select2`). 
+ Each control directory should contain the `src` directory.
+ The `src` directory should contain the `DotVVM.Contrib.Controls.sln` solution. We plan to create NuGet packages for each control soon.
+ Each solution should have the `DotVVM.Contrib.Controls` class library that contains the control itself (e.g. `Select2.cs`), and the  extension method which registers the control congiruration (e.g. `Select2Extensions.cs` with the `RegisterSelect2Configuration` method.
+ Each solution should have the `DotVVM.Contrib.Samples` web app that shows how the control is used, and allows to manually check the control functionality.
+ Each solution should have the `DotVVM.Contrib.Tests` project with Selenium UI tests (and other unit or integration tests, if necessary).
+ If the control register some resources, these should have unique names. For example, the `Select2` control registers the resources `select2` (a JS library), `select2-css` (a CSS file for Select2) and `dotvvm-contrib-select2` (a JS file that registers Knockout JS binding for the DotVVM control). All controls should follow this convention.
+ All Knockout bindings should start with `dotvvm-contrib-ControlName` or `dotvvm-contrib-ControlName-PropertyName`, if the binding represents the specific property of the control.
+ Each control should be registered using the tag prefix `dc` (which stands for dotvvm-contrib).
+ Each control directory should contain the `readme.md` file which briefly describes the control and shows samples of its usage. 

We are very grateful for your contributions. We hope that you'll find this repository useful.
