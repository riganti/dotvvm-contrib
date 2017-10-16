using DotVVM.Framework.Binding;
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

        public bool LimitToList
        {
            get { return (bool)GetValue(LimitToListProperty); }
            set { SetValue(LimitToListProperty, value); }
        }

        public static readonly DotvvmProperty LimitToListProperty = DotvvmProperty.Register<bool, TypeAhead>(c => c.LimitToList);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("type", "text");

            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-DataSource", this, DataSourceProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-SelectedValue", this, SelectedValueProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-DisplayMember", KnockoutHelper.MakeStringLiteral(DisplayMember));
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-ValueMember", KnockoutHelper.MakeStringLiteral(ValueMember));
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-LimitToList", this, LimitToListProperty);

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
