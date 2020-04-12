# EditableForm

This control wraps its content in a panel with the Edit/Save/Cancel buttons, and thus prevents the user from editing the data by mistake. 

All controls in the panel are disabled unless the Edit button is pressed, and the user can cancel at any time.

## ViewModel

The control depends on `IEditableFormViewModel` interface which declares the following members:

```
// determines whether the form is in editable state
bool IsEditable { get; }

// switches the form into the edit mode
void Edit();

// switches the form back into the read-only mode
void Cancel();

// saves the changes and switches the form back into the read-only mode
Task Save();
```

For your convenience, we've added a ready-made viewmodel that you can use - just pass the delegate to load and save the data.

```
public class SampleViewModel1 : DotvvmViewModelBase 
{

    public EditableFormViewModel<CustomerData> CustomerForm { get; set; }


    public Sample1ViewModel()
    {
        CustomerForm = new EditableFormViewModel<CustomerData>(LoadCustomerAsync, SaveCustomerAsync);
    }

    private Task<CustomerData> LoadCustomerAsync()
    {
        // ...
    }

    private Task SaveCustomerAsync(CustomerData customer)
    {
        // ...
    }
    
}
```

## Sample 1: Default Template

The default template uses some classes from **Bootstrap 4**, but it is not difficult to define them even if you don't have Bootstrap in your app.

```DOTHTML
<dc:EditableForm DataContext="{value: CustomerForm}">

    <dot:TextBox Text="{value: Data.FirstName}" />

</dc:EditableForm>
```

<br />

## Sample 2: Redefining the template

When registering the `EditableForm` control in `DotvvmStartup.cs`, there is an option to specify a custom template that replaces the built-in one.

The template must:
* assume it will be used in a place that implements `IEditableFormViewModel`. 
* contain a control with `ID="TemplateHost"` - the content specified in `<ContentTemplate>` will go there.

```CSHARP
config.AddContribEditableFormConfiguration(new EditableFormOptions() { 
    MarkupFilePath = "use your own *.dotcontrol markup file path";
});
```

<br />

## Sample 3: Redefining the template for a single occurence

If you don't want to redefine the template globally, you can create your own control that inherits the logic of this control.
 
Just create a `.dotcontrol` file that has a viewmodel of type `IEditableFormViewModel` and uses `EditableFormControl` as its base type.

```DOTHTML
@viewModel DotVVM.Contrib.Model.IEditableFormViewModel, DotVVM.Contrib.EditableForm
@baseType DotVVM.Contrib.EditableForm.EditableFormControl, DotVVM.Contrib.EditableForm

<div class="card" Validation.Target="{value: _this}">
    <div class="card-header">
        <dot:Button Text="Edit"
                    Click="{command: Edit()}"
                    Visible="{value: !IsEditable}"
                    Validation.Enabled="false" 
                    class="btn btn-info editableform__button-edit" />

        <dot:Button Text="Save"
                    Click="{command: Save()}"
                    Visible="{value: IsEditable}"
                    class="btn btn-primary editableform__button-save" />

        <dot:Button Text="Cancel"
                    Click="{command: Cancel()}"
                    Visible="{value: IsEditable}"
                    Validation.Enabled="false"
                    class="btn btn-danger editableform__button-cancel" />
    </div>

    <div class="card-body" ID="TemplateHost" FormControls.Enabled="{value: IsEditable}">
    </div>
</div>
```

Then, you can just register your new control:

```CSHARP
config.Markup.Controls.Add(new DotvvmControlConfiguration()
{
    TagPrefix = "dc",
    TagName = "EditableFormWithCustomTemplate",
    Src = "Views/EditableFormWithCustomTemplate.dotcontrol"
});
```