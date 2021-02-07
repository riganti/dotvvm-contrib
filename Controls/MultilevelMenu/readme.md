# MultilevelMenu

Display a server-rendered multi-level menu that can be styled using CSS. You can use [craigerskine/css-menu](https://github.com/craigerskine/css-menu) as an example.

The control produces the following HTML code:

```
<ul class="nav-menu">
  <li><a href="..." class="nav-active">...</a></li>
  <li>
    <a href="...">...</a>
	<ul>
	  <li><a href="...">...</a></li>
	  <li><a href="...">...</a></li>
	</ul>
  </li>
  <li><a href="...">...</a></li>
</ul>
```

## Sample 1: Horizontal menu

In order to render the menu, create a list of `MenuItem` objects that represents the menu items.

* `Text` is the text that will be displayed.
* `NavigateUrl` is a relative or absolute URL for the menu item. Use `MenuItem.BuildUrl` to generate it from a route name and parameters (there are optional arguments to generate query string or suffix).
* `IsActive` indicates whether the menu item is highlighted. We recommend comparing the current route name to the route name used to build the URL.

```DOTHTML
<dc:MultilevelMenu Menu="{resource: Menu}" />
```

```CSHARP
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
```

<br />

## Sample 2: Vertical menu

You can choose to render a vertical menu:

```DOTHTML
<dc:MultilevelMenu Menu="{resource: Menu}" Direction="Vertical" />
```

<br />

## Sample 3: Custom data and templates

If you want to specify additional properties in the `MenuItem` (for example an icon), use `MenuItem<T>` where `T` is your model class for additional data of the menu item.

Then, you can specify the `ItemTemplate` and use data-binding to present the additional info:

```DOTHTML
<dc:MultilevelMenu DataSource="{resource: Menu}">
	<ItemTemplate>
		<i class="{resource: ExtraData.IconClass}"></i>
		{{resource: Text}}
	</ItemTemplate>
</dc:MultilevelMenu>
```

```CSHARP
public List<MenuItem<IconData>> Menu => new List<MenuItem<IconData>>()
{
	new MenuItem<IconData>()
	{
		Text = "Sample 1",
		NavigateUrl = MenuItem.BuildUrl(Context, "Sample1"),
		IsActive = Context.Route.RouteName == "Sample1",
		ExtraDaa = new IconData() 
		{
		    IconClass = "fas fa-arrow-alt-circle-up"
		}
	},
	...
};
```

<br />

## Limitations

The menu is rendered on the server. If you make any changes in the `Menu` collection on postback, these changes won't be applied. If you want to build dynamic menus, we recommend to use the `Repeater` control instead.

The `Menu` property must be bound by `{resource: ...}` binding - the value binding won't work. The same applies for the `ItemTemplate` - use the `resource` binding everywhere inside the template. We recommand avoiding making postbacks from the template, although it should work.

The object defining the menu doesn¨t need to be included in the viewmodel - we recommend to mark it with `[Bind(Direction.None)]`.