# PolymorphTemplateSelector

[DotVVM](https://github.com/riganti/dotvvm) doesn't support polymorphism in viewmodels natively, but this control can help to use "polymorph" objects and render different templates for each of the possible types.

To use this control, you will need a **special object** which contains all of the possible variants represented as properties. When you use this object, **only one of the properties will be set - the others should be `null`**.

```CSHARP
public class QuestionEntry
{
	// only one of these properties will be set - others will be null
	public YesNoQuestion YesNo { get; set; }
	public NumberQuestion Number { get; set; }
	public ChoiceQuestion Choice { get; set; }
	public OpenTextQuestion OpenText { get; set; }
}
```

The `PolymorphTemplateSelector` contains a list of `PolymorphTemplate` controls which define a template for each possible option. 

When used on the page, the control will display the template for the property which contains some value. (If there are more properties with a non-null value, the first of them will be used.)

You can also specify a `FallbackTemplate` which defines what the user will see if no property is set.

## Sample 1: Basic use

Define templates for each of the options.

```DOTHTML
<dc:PolymorphTemplateSelector>
	<!-- the DataContext property controls which template will be shown -->
	
	<dc:PolymorphTemplate DataContext="{value: YesNo}">
		<dot:RadioButton CheckedItem="{value: Value}" CheckedValue="{value: true}" Text="yes" />
		<dot:RadioButton CheckedItem="{value: Value}" CheckedValue="{value: false}" Text="no" />
	</dc:PolymorphTemplate>
	
	<dc:PolymorphTemplate DataContext="{value: Choice}">
		<dot:ComboBox DataSource="{value: Choices}" SelectedValue="{value: Value}" />
	</dc:PolymorphTemplate>
	
	<dc:PolymorphTemplate DataContext="{value: Number}">
		<dot:TextBox Type="Number" Text="{value: Value}" min="{value: Min}" max="{value: Max}" />
	</dc:PolymorphTemplate>
	
	<dc:PolymorphTemplate DataContext="{value: OpenText}">
		<dot:TextBox Type="MultiLine" Text="{value: Value}" />
	</dc:PolymorphTemplate>
</dc:PolymorphTemplateSelector>
```

<br />

## Sample 2: Use in Repeater

You don't need to worry about using the control inside `Repeater` - the templates will be added to the page only once (DotVVM de-duplicates them by their content hash).

```DOTHTML
<dot:Repeater DataSource="{value: Questions}">
	<dc:PolymorphTemplateSelector>
		...
	</dc:PolymorphTemplateSelector>
</dot:Repeater>
```

<br />

## Sample 3: Fallback template

If no template is matched (`DataContext` of all `PolymorphTemplate` objects is `null`), the control will render the `FallbackTemplate`:

```DOTHTML
<dc:PolymorphTemplateSelector>
	<Templates>	
		<dc:PolymorphTemplate DataContext="{value: YesNo}">
			...
		</dc:PolymorphTemplate>
	</Templates>
	<FallbackTemplate>
		<dot:Button Text="Create Yes/No question" Click="{command: InitYesNo()}" />
		<dot:Button Text="Create Choice question" Click="{command: InitChoice()}" />
		...
	</FallbackTemplate>
</dc:PolymorphTemplateSelector>
```