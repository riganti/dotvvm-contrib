using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{

    public class MenuItem
    {
        public string Title { get; set; }
		public string Url { get; set; }
    }
	public class Sample1ViewModel : MasterViewModel
	{
		public MenuItem[] MenuItems { get; set; } = new MenuItem[] {
			new MenuItem { Title = "This site", Url = "." },
			new MenuItem { Title = "DotVVM Repo", Url = "https://github.com/riganti/dotvvm/" },
			new MenuItem { Title = "DotVVM Contrib Repo", Url = "https://github.com/riganti/dotvvm-contrib/" },
		};
	}
}

