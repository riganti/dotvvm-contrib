using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
	{
        public DateTime? Date1 { get; set; }

        public DateTime Date2 { get; set; } = DateTime.Now;

        public override Task Init()
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            return base.Init();
        }
    }
}

