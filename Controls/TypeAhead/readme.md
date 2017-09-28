# TypeAhead

Provides a wrapper over the popular [typeahead.js](https://twitter.github.io/typeahead.js/) library.

## Sample 1: Binding to a collection of strings

You can bind the control to a collection of `string`s:

```DOTHTML
<dc:TypeAhead DataSource="{value: CountryNames}" 
              SelectedValue="{value: SelectedCountryName}" />
<br />
Selected Value: <span class="result">{{value: SelectedCountryName}}</span>
```

<br />

## Sample 2: Binding to a collection of objects

You can bind the control to a collection of any objects and use the `DisplayMember` and `ValueMember` properties to specify, which fields should be used as the item text and item value. This approach is similar to the [ComboBox](https://www.dotvvm.com/docs/controls/builtin/ComboBox/1-0) control.

```DOTHTML
<dc:TypeAhead DataSource="{value: Countries}" 
              SelectedValue="{value: SelectedCountryId}" 
              DisplayMember="Name" 
              ValueMember="Id" />
<br />
Selected object ID: <span class="result">{{value: SelectedCountryId}}</span>
```
