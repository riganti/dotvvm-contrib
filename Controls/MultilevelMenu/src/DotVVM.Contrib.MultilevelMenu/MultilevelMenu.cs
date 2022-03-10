using System.Collections.Generic;
using System.Linq;
using DotVVM.Contrib.MultilevelMenu.Model;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.MultilevelMenu
{
    /// <summary>
    /// Renders a multi-level menu.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(MultilevelMenu.ItemTemplate))]
    public class MultilevelMenu : HtmlGenericControl
    {

        /// <summary>
        /// Gets or sets the direction of the menu items.
        /// </summary>
        public MultilevelMenuDirection Direction
        {
            get { return (MultilevelMenuDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly DotvvmProperty DirectionProperty
            = DotvvmProperty.Register<MultilevelMenuDirection, MultilevelMenu>(c => c.Direction, MultilevelMenuDirection.Horizontal);

        /// <summary>
        /// Gets or sets a list of menu items to be displayed.
        /// </summary>
        [MarkupOptions(AllowBinding = false, Required = true)]
        public IEnumerable<IMenuItem> DataSource
        {
            get { return (IEnumerable<IMenuItem>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DotvvmProperty DataSourceProperty
            = DotvvmProperty.Register<IEnumerable<IMenuItem>, MultilevelMenu>(c => c.DataSource, null);

        /// <summary>
        /// Gets or sets a template that will be used for the content of a menu item.
        /// </summary>
        [MarkupOptions(MappingMode = MappingMode.InnerElement)]
        [ControlPropertyBindingDataContextChange(nameof(DataSource), 0)]
        [CollectionElementDataContextChange(1)]
        public ITemplate ItemTemplate
        {
            get { return (ITemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public static readonly DotvvmProperty ItemTemplateProperty
            = DotvvmProperty.Register<ITemplate, MultilevelMenu>(c => c.ItemTemplate, null);


        public MultilevelMenu() : base("ul")
        {
            SetValue(RenderSettings.ModeProperty, RenderMode.Server);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "nav-menu", append: true);

            if (Direction == MultilevelMenuDirection.Vertical)
            {
                writer.AddAttribute("class", "nav-vertical", append: true);
            }

            base.AddAttributesToRender(writer, context);
        }

        protected override void OnLoad(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.MultilevelMenu");

            DataBind(context);

            base.OnLoad(context);
        }

        protected virtual void DataBind(IDotvvmRequestContext context)
        {
            Children.Clear();

            if (DataSource == null)
            {
                return;
            }

            BuildLevel(context, this, DataSource);
        }

        private void BuildLevel(IDotvvmRequestContext context, HtmlGenericControl container, IEnumerable<IMenuItem> menuItems)
        {
            var stack = container.GetDataContextType();
            foreach (var item in menuItems)
            {
                // create <li>
                var li = new HtmlGenericControl("li");
                li.SetDataContextType(DataContextStack.Create(item.GetType(), stack));
                li.DataContext = item;
                container.Children.Add(li);

                // create <a>
                var link = new HtmlGenericControl("a");
                link.Attributes.Set("href", item.NavigateUrl);
                if (item.IsActive)
                {
                    link.Attributes.Set("class", "nav-active");
                }
                li.Children.Add(link);

                // create the content
                if (ItemTemplate != null)
                {
                    ItemTemplate.BuildContent(context, link);
                }
                else
                {
                    link.InnerText = item.Text;
                }

                // create child item list
                if (item.ChildItems?.Any() == true)
                {
                    var ul = new HtmlGenericControl("ul");
                    li.Children.Add(ul);
                    BuildLevel(context, ul, item.ChildItems);
                }
            }
        }
    }
}
