using System;
using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.Compilation.Validation;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Represents a polymorph template definition.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(ContentTemplate))]
    public class PolymorphTemplate : DotvvmBindableObject 
    {

        /// <summary>
        /// Gets or sets a template representing the template content.
        /// </summary>
        [MarkupOptions(Required = true, AllowBinding = false, MappingMode = MappingMode.InnerElement)]
        public ITemplate ContentTemplate
        {
            get { return (ITemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        public static readonly DotvvmProperty ContentTemplateProperty
            = DotvvmProperty.Register<ITemplate, PolymorphTemplate>(c => c.ContentTemplate, null);



        [ControlUsageValidator]
        public static IEnumerable<ControlUsageError> ValidateUsage(ResolvedControl control)
        {
            if (!control.TryGetProperty(DataContextProperty, out var setter) 
                || setter is not ResolvedPropertyBinding binding
                || binding.Binding.Binding is not IValueBinding)
            {
                yield return new ControlUsageError("The DataContext property of PolymorphTemplate must be set!", control.DothtmlNode);
            }
        }


    }
}
