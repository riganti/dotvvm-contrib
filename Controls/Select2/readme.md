# Select2

Wraps the popular [Select2](https://select2.github.io/) library. 

Supports the DotVVM data-binding using the `DataSource` property. 

This control requires **jQuery** to be registered as a DotVVM resource with resource name: `jquery`.


## Sample 1: Collection of Strings

You can bind the control to a collection of strings. The `SelectedValues` property retrieves a list of selected items.

```DOTHTML
<dc:Select2 DataSource="{value: CityNames}" SelectedValues="{value: SelectedCityNames}" />
```

<br />

## Sample 2: Collection of Objects

Similar to the built-in `ComboBox` control, you can specify the `DataMember` and `ValueMember` properties and bind the control to a collection of objects - the `DisplayMember` specifies the property of the object 
which is displayed, and the `ValueMember` specified the property of the object which will be used in the `SelectedValues` collection.

```DOTHTML
<dc:Select2 DataSource="{value: Cities}" SelectedValues="{value: SelectedCityIds}" DisplayMember="Name" ValueMember="Id" />
```

<br />

## Sample 3: single-value select

The usage is exactly the same as built-in `ComboBox` control. The rendered select can viewed in the [Select2 Docs](https://select2.org/getting-started/basic-usage#single-select-boxes).

```DOTHTML
<dc:Select2Single DataSource="{value: Cities}" SelectedValue="{value: SelectedCityId}" DisplayMember="Name" ValueMember="Id" Placeholder="Example Placeholder" AllowClear="true" />
```