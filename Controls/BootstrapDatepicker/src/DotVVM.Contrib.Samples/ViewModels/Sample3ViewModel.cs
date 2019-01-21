using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample3ViewModel : MasterViewModel
    {
        public DateTime Date1 { get; set; } = DateTime.Today;

        public override Task Init()
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            return base.Init();
        }
    }
}

