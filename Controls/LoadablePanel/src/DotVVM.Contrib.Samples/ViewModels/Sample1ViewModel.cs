using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{
        public string Data1 { get; set; }
        public string Data2 { get; set; }

        [AllowStaticCommand]
        public async Task<string> LoadData1()
        {
            await Task.Delay(2000);
            return "Loaded content in 2000 ms";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData2()
        {
            await Task.Delay(3000);
            return "Loaded content in 3000 ms";
        }
    }
}

