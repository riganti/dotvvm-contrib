using System.Collections.Generic;
using DotVVM.Contrib.MultilevelMenu.Model;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.MultilevelMenu.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
    {
        [Bind(Direction.None)]
        public List<MenuItem> Menu => new List<MenuItem>()
        {
            new MenuItem()
            {
                Text = "Sample 1",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample1"),
                IsActive = Context.Route.RouteName == "Sample1"
            },
            new MenuItem()
            {
                Text = "Sample 2",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample2"),
                IsActive = Context.Route.RouteName == "Sample2",
                ChildItems =
                {
                    new MenuItem()
                    {
                        Text = "Sample 2 Child 1",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child1")
                    },
                    new MenuItem()
                    {
                        Text = "Sample 2 Child 2",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child2")
                    },
                    new MenuItem()
                    {
                        Text = "Sample 2 Child 3",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child3")
                    }
                }
            },
            new MenuItem()
            {
                Text = "Sample 3",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample3"),
                IsActive = Context.Route.RouteName == "Sample3"
            }
        };

    }
}

