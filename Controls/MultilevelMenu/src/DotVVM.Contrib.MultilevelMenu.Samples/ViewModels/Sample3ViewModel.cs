using System.Collections.Generic;
using DotVVM.Contrib.MultilevelMenu.Model;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.MultilevelMenu.Samples.ViewModels
{
    public class Sample3ViewModel : MasterViewModel
    {


        [Bind(Direction.None)]
        public List<MenuItem<IconData>> Menu => new List<MenuItem<IconData>>()
        {
            new MenuItem<IconData>()
            {
                Text = "Sample 1",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample1"),
                IsActive = Context.Route.RouteName == "Sample1",
                ExtraData = new IconData()
                {
                    IconClass = "fas fa-address-book"
                }
            },
            new MenuItem<IconData>()
            {
                Text = "Sample 2",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample2"),
                IsActive = Context.Route.RouteName == "Sample2",
                ExtraData = new IconData()
                {
                    IconClass = "fas fa-arrow-alt-circle-up"
                },
                ChildItems =
                {
                    new MenuItem<IconData>()
                    {
                        Text = "Sample 2 Child 1",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child1"),
                        ExtraData = new IconData()
                        {
                            IconClass = "fas fa-balance-scale"
                        }
                    },
                    new MenuItem<IconData>()
                    {
                        Text = "Sample 2 Child 2",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child2"),
                        ExtraData = new IconData()
                        {
                            IconClass = "far fa-address-card"
                        }
                    },
                    new MenuItem<IconData>()
                    {
                        Text = "Sample 2 Child 3",
                        NavigateUrl = MenuItem.BuildUrl(Context, "Sample2_Child3"),
                        ExtraData = new IconData()
                        {
                            IconClass = "fas fa-biking"
                        }
                    }
                }
            },
            new MenuItem<IconData>()
            {
                Text = "Sample 3",
                NavigateUrl = MenuItem.BuildUrl(Context, "Sample3"),
                IsActive = Context.Route.RouteName == "Sample3",
                ExtraData = new IconData()
                {
                    IconClass = "fas fa-book-open"
                }
            }
        };


        public class IconData
        {
            public string IconClass { get; set; }
        }
    }
}

