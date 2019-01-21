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

        public void SetDate1()
        {
            Date1 = new DateTime(2000, 1, 20);
        }

        public void SetDate1Null()
        {
            Date1 = null;
        }

        public void SetDate2()
        {
            Date2 = new DateTime(2000, 1, 30);
        }
    }
}

