using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotVVM.Contrib.Select2
{
    internal static class KnockoutExtensions
    {
        public static void AddSimpleBinding(this KnockoutBindingGroup group, string name, DotvvmControl control, DotvvmProperty property)
        {
            var binding = control.GetValueBinding(property);
            if (binding == null)
            {
                string value = JsonConvert.ToString(property.GetValue(control));
                group.Add(name, value, false);

            }
            else
            {
                string expression = control.GetValueBinding(property).GetKnockoutBindingExpression(control);
                group.Add(name, $"{expression}");

            }
        }
    }
}
