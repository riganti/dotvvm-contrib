# CrystalReportViewer

Wraps the ASP.NET Web Forms control CrystalReportViewer inside an `iframe`. The control is used to show data as a report using Crystal Reports.



## Sample 1: Basic usage

In order to used this control you have to specify the path to the Crystal Report file in `CrystalReportFile` property. 

The `iframe` points to `CrystalReportViewerPage.aspx` url by default but you can specify a different one with the `ReportPageUrl` property.

Every property is sent as a query string parameter to the ASP.NET page.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}" />
```

<br />

## Sample 2: Resizing the report

If you want to resize the report set the `BestFitPage` property to `false` and then you can use the `Width` and `Height` property.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}"  BestFitPage="false" Width="800px" Height="600px"/>
```

<br />

## Sample 3: Other properties

Additionally you can set `DisplayToolbar`, `DisplayPage` and `DisplayStatusbar` which hide and show parts of the report.

You can also set the `ExtraCssFileUrl` property which coresponds with the same property of the ASP.NET control.

```DOTHTML
<dc:CrystalReportViewer CrystalReportFile="{value: CrystalReportFile}"  DisplayToolbar="false" DisplayPage="true" DisplayStatusbar="false" ExtraCssFileUrl="../Styles/CrystalReportViewer.css"/>
```


