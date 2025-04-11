using System.Linq;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.SvgParser.Samples.ViewModels
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

