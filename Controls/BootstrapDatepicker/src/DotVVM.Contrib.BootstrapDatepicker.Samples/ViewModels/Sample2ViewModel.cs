using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DotVVM.Contrib.BootstrapDatepicker.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {
        public DateTime Date1 { get; set; } = DateTime.Today;

        public string Lang { get; set; } = "fr";

        public override Task Init()
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            return base.Init();
        }

        public void SetDate1()
        {
            Date1 = new DateTime(2000, 1, 20);
        }
    }
}

