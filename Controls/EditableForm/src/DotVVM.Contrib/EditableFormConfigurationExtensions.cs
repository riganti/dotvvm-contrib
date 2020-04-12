using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Contrib.EditableForm;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
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
