using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a TypeAhead input field.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class TypeAhead : Selector
    {

        public TypeAhead() : base("input")
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("type", "text");

            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-DataSource", this, DataSourceProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-SelectedValue", this, SelectedValueProperty);
            if (ItemValueBindingProperty.IsSet(this))
            {
                writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-ValueMember", ItemValueBinding.KnockoutExpression.ToDefaultString());
            }

            if (ItemTextBindingProperty.IsSet(this))
            {
                writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-DisplayMember", ItemTextBinding.KnockoutExpression.ToDefaultString());
            }

            var selectionChangedBinding = GetCommandBinding(SelectionChangedProperty);
            if (selectionChangedBinding != null)
            {
                writer.AddAttribute("onchange", KnockoutHelper.GenerateClientPostBackScript(nameof(SelectionChanged), selectionChangedBinding, this, isOnChange: true, useWindowSetTimeout: true));
            }

            base.AddAttributesToRender(writer, context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.TypeAhead");

            base.OnPreRender(context);
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderSelfClosingTag(TagName);
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }
    }
}
