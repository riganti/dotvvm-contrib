using System.Collections.Generic;

namespace DotVVM.Contrib.Select2.Samples.ViewModels
{
	public class Sample2ViewModel : MasterViewModel
	{

	    public List<string> Letters { get; set; } = new List<string>() { "a", "b", "c", "d" };

	    public List<string> SelectedLetters { get; set; } = new List<string>();

	    public void OnSelected()
	    {
	        NumberOfRequests++;
	    }

	    public int NumberOfRequests { get; set; }
	}
}

