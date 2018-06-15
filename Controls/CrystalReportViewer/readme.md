# CrystalReportViewer

This DotVVM control hosts the ASP.NET Web Forms version of `CrystalReportViewer` inside an `iframe`. The control is used to render Crystal Reports in DotVVM web pages.

Notice: This control works only with **full .NET Framework** and with classic ASP.NET Application projects (ASP.NET Core and new project system is not supported).

[SAP Crystal Reports for Visual Studio](http://www.crystalreports.com/crvs/confirm/) has to be installed on the developer machine.



## Sample 1: Basic usage

In order to use this control, you have to specify the path to the Crystal Report file in `CrystalReportFile` property. 

The `iframe` points to `CrystalReportViewerPage.aspx` URL, which must be included in the project and is a part of the NuGet package. If you move this page to another location, you can specify the location using the `ReportPageUrl` property.

Every property set to the DotVVM version of the control is passed as a query string parameter to the ASP.NET page in the `iframe`.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}" />
```

<br />

## Sample 2: Resizing the report

If you want to resize the report, set the `BestFitPage` property to `false`. Then you can use the `Width` and `Height` properties.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}"  BestFitPage="false" Width="800px" Height="600px"/>
```

<br />

## Sample 3: Other properties

Additionally, you can set `DisplayToolbar`, `DisplayPage` and `DisplayStatusbar` which can hide and show parts of the report.

You can also set the `ExtraCssFileUrl` property which defines URL to a custom CSS stylesheet that will be used in the report.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}"  DisplayToolbar="false" DisplayPage="true" DisplayStatusbar="false" ExtraCssFileUrl="../Styles/CrystalReportViewer.css"/>
```
