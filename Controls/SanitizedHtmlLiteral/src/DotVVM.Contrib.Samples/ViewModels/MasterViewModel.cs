using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class MasterViewModel : DotvvmViewModelBase
	{

	    public string[] AllSamples => Context.Configuration.RouteTable
                                          .Where(r => !r.RouteName.StartsWith("_"))
                                          .Select(r => "/" + r.RouteName)
                                          .OrderBy(r => r)
                                          .ToArray();

	}
}

