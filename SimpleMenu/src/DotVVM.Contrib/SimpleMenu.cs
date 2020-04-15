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

        public SimpleMenu() : base("div")
        {
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            base.OnLoad(context);
        }
    }
}
