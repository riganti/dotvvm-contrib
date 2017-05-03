using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Switch_Sample1ViewModel : MasterViewModel
	{

	    public bool SwitchValue { get; set; }

	    public bool Enabled { get; set; } = true;

	    public void SetValue()
	    {
	        SwitchValue = !SwitchValue;
	    }
    }
}

