using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib.EditableForm
{
    public static class EditableFormConfigurationExtensions
    {

        public static void AddContribEditableFormConfiguration(this DotvvmConfiguration config, EditableFormOptions options = null)
        {
            if (options == null)
            {
                options = new EditableFormOptions();
            }

            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                TagPrefix = "dc",
                TagName = "EditableForm",
                Src = options.MarkupFilePath
            });
        }

    }
}
