using System;
using System.Text.Json;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib.NoUiSlider
{
    internal static class KnockoutExtensions
    {
        public static void AddNegation(this KnockoutBindingGroup group, string name, DotvvmControl control, DotvvmProperty property, Func<bool> nullBindingAction)
        {
            var binding = control.GetValueBinding(property);
            if (binding == null)
            {
                group.Add(name, (!nullBindingAction()).ToString().ToLower());
            }
            else
            {
                string expression = control.GetValueBinding(property).GetKnockoutBindingExpression(control);
                group.Add(name, $"!{expression}()");
            }
        }

        public static void AddSimpleBinding(this KnockoutBindingGroup group, string name, DotvvmControl control, DotvvmProperty property)
        {
            var binding = control.GetValueBinding(property);
            if (binding == null)
            {
                string value = JsonSerializer.Serialize(property.GetValue(control));
                group.Add(name, value, false);

            }
            else
            {
                string expression = control.GetValueBinding(property).GetKnockoutBindingExpression(control);
                group.Add(name, $"{expression}");

            }
        }

        public static void AddExtender(this KnockoutBindingGroup group, string name, DotvvmControl control, DotvvmProperty property, string extenderName)
        {
            var binding = control.GetValueBinding(property);
            if (binding == null)
            {
                throw new NotSupportedException();
                //group.Add(name, (!nullBindingAction()).ToString().ToLower());
            }
            else
            {
                string expression = control.GetValueBinding(property).GetKnockoutBindingExpression(control);
                group.Add(name, $"{expression}.{extenderName}");
            }
        }
    }
}
