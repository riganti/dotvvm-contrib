# TemplateSelector

This control can select and render one template out of the given template list depending on a key specified using data-binding.

## Sample 1: Basic Usage

```DOTHTML
<dc:TemplateSelector SelectedKeyBinding="{value: IsCompany ? "company" : "person">
	<dc:TemplateChoice Key="person">
		<p>First Name: {{value: FirstName}}</p>
		<p>Last Name: {{value: LastName}}</p>
	</dc:TemplateChoice>
	<dc:TemplateChoice Key="company">
		<p>Company Name: {{value: CompanyName}}</p>
		<p>Company ID: {{value: CompanyID}}</p>
	</dc:TemplateChoice>
</dc:TemplateSelector>
```

<br />

## Sample 2: Usage in Repeater

Depending on the type of the collection item, you can render a specified template:

```DOTHTML
<table>
	<dot:Repeater DataSource="{value: Items}" WrapperTagName="tbody">
		<tr>
			<td>
				<dc:TemplateSelector SelectedKeyBinding="{value: Type}">
				
					<dc:TemplateChoice Key="Heading">
						<fieldset class="fs-h">
							<legend>Heading</legend>
							<dot:TextBox Text="{value: Text}" />
						</fieldset>
					</dc:TemplateChoice>
					
					<dc:TemplateChoice Key="Paragraph">
						<fieldset class="fs-p">
							<legend>Paragraph</legend>
							<dot:TextBox Text="{value: Text}" />
						</fieldset>
					</dc:TemplateChoice>
					
					<dc:TemplateChoice Key="Item">
						<fieldset class="fs-i">
							<legend>Item</legend>
							Title: <dot:TextBox Text="{value: Text}" /><br />
							Description: <dot:TextBox Text="{value: Text2}" /><br />
							URL: <dot:TextBox Text="{value: Url}" />
						</fieldset>
					</dc:TemplateChoice>
					
				</dc:TemplateSelector>
			</td>
		</tr>
	</dot:Repeater>
</table>
```