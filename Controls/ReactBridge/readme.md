# ReactBridge

This control brings possibility of usage react, react-dom, react-trend, react-numeric-input components in your dotvvm views. Part of this contrib control is also ChartList control that allow to create Line, Bar and Pie charts.


## Sample 1: React Trend control

You can do this:

```DOTHTML
    <dc:ReactBridge Name="Trend.default" data="{value: Data}" smooth="{value: true}" autoDraw="{value: true}" autoDrawDuration= {value: 3000}"
		autoDrawEasing="ease-out"
    width="{resource: 600}" height="{resource: 250}"
    padding="{value: 10}"
		gradient="{value: Gradient}"
		radius="{value: 10}"
		strokeWidth="{value: 10}"
		strokeLinecap="{value: 'round'}" />
```

<br />

## Sample 2: NumericInput control

You can do also this:

```DOTHTML
    <dot:Repeater DataSource="{value: Data}">
       <dc:ReactBridge Name="NumericInput" update:onChange="{value: _this}" value="{value: _this}" />
    </dot:Repeater>
```

## Sample 3: Chartist controls

```DOTHTML

    <dc:Chartist Type="Line" Data="{value: SingleSeriesChartData}"  Options=" { low: 0, showArea: true } " />

    <dc:Chartist Type="Pie" Data="{value: SingleSeriesChartDataWithoutLabels}" Class="BarChart"/>

    <dc:Chartist Type="Bar" Data="{value: BarChartData}"  
                    Options=" { stackBars: true } " />

```
