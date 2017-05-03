using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample2ViewModel : MasterViewModel
	{

	    public List<string> Letters { get; set; } = new List<string>() { "a", "b", "c", "d" };

	    public List<string> SelectedLetters { get; set; } = new List<string>() { "b" };

	    public void OnSelected()
	    {
	        SelectedLetters = new List<string>() { "d" };
	        NumberOfRequests++;
	    }

	    public int NumberOfRequests { get; set; }
	}
}

