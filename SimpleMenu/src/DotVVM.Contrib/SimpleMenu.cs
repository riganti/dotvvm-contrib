using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class SimpleMenu : HtmlGenericControl
    {
        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DotvvmProperty DataSourceProperty =
            DotvvmProperty.Register<object, SimpleMenu>(nameof(DataSource));


        [ControlPropertyTypeDataContextChange(nameof(DataSource), order: 0)]
        [CollectionElementDataContextChange(order: 1)]
        public IValueBinding<string> ItemUrlBinding
        {
            get { return (IValueBinding<string>)GetValue(ItemUrlBindingProperty); }
            set { SetValue(ItemUrlBindingProperty, value); }
        }
        public static readonly DotvvmProperty ItemUrlBindingProperty =
            DotvvmProperty.Register<IValueBinding<string>, SimpleMenu>(nameof(ItemUrlBinding));

        [ControlPropertyTypeDataContextChange(nameof(DataSource), order: 0)]
        [CollectionElementDataContextChange(order: 1)]
        public IValueBinding<string> ItemTitleBinding
        {
            get { return (IValueBinding<string>)GetValue(ItemTitleBindingProperty); }
            set { SetValue(ItemTitleBindingProperty, value); }
        }
        public static readonly DotvvmProperty ItemTitleBindingProperty =
            DotvvmProperty.Register<IValueBinding<string>, SimpleMenu>(nameof(ItemTitleBinding));

        public SimpleMenu() : base("nav")
        {
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            var repeater = new Repeater();
            repeater.SetBinding(Repeater.DataSourceProperty, GetBinding(DataSourceProperty));
            repeater.WrapperTagName = "ul";
            repeater.ItemTemplate = new DelegateTemplate(_ => {
                var link = new HtmlGenericControl("a");
                link.Attributes.Add("href", ItemUrlBinding);
                link.SetBinding(HtmlGenericControl.InnerTextProperty, ItemTitleBinding);
                var item = new HtmlGenericControl("li") { Children = { link } };
                return item;
            });

            Children.Add(repeater);
        }
    }
}
