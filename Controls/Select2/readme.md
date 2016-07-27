# Select2

Wraps the popular [Select2](https://select2.github.io/) library. 

Supports the DotVVM data-binding using the `DataSource` property. 


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