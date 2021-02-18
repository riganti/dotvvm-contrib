# ReactBridge

This control brings possibility to use react components in your DotVVM views.
In samples you can find react-trend or react-numeric-input components.

|Property|Description|
|---|---|
| ```Name```   | should be the same as react component name that will be rendered |
| ```update:``` prefix   | calls updates on properties in ViewModel when event callback is executed |
| Individual ```<dc:ReactBridge />``` properties   |  value bindings are passed to component props with identical name  |

## Sample 1:

You can do this:

```DOTHTML
    <dc:ReactBridge Name="Trend.default"
    	data="{value: Data}"
    	smooth="{value: true}"
    	autoDraw="{value: true}"
    	autoDrawDuration= {value: 3000}"
    	autoDrawEasing="ease-out"
    	width="{resource: 600}" height="{resource: 250}"
    	padding="{value: 10}"
    	gradient="{value: Gradient}"
    	radius="{value: 10}"
    	strokeWidth="{value: 10}"
    	strokeLinecap="{value: 'round'}" />
```

<br />

## Sample 2:

You can do also this to update ```Data``` property on onChange callback:

```DOTHTML
    <dot:Repeater DataSource="{value: Data}">
       <dc:ReactBridge Name="NumericInput" update:onChange="{value: _this}" value="{value: _this}" />
    </dot:Repeater>
```

