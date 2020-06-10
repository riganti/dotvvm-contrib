using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample4ViewModel : MasterViewModel
    {
        public DateTime? Date1 { get; set; }

        public string Changed { get; set; } = "";
    }
}

