# Slider

Wraps the [NoUiSlider](https://refreshless.com/nouislider/) javascript component. 

Supports the DotVVM data-binding for the `Value` and `Enabled` properties.

The package contains two basic controls

- Slider - classic trackbar with dragable handle for changing value in specified interval.
- Switch - trackbar degraded to switch between `true` and `false` values. It is alternative to classic checkbox.


## Sample 1: Slider

```DOTHTML
<dc:Slider MaxValue="100" MinValue="0" Step="10" Value="{value: SliderValue}" Enabled="{value: Enabled}" Orientation="Horizontal" Direction="LeftToRight">
```

<br />

## Sample 2: Switch

```DOTHTML
<dc:Switch Value="{value: SwitchValue}" Enabled="{value: Enabled}" Orientation="Horizontal" Direction="LeftToRight"></dc:Switch>
```