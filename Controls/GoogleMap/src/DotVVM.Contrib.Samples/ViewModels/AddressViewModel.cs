using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class AddressViewModel : MasterViewModel
    {
        public string Address { get; set; } = "Chicken Dinner Rd Idaho 83607, USA";

        public void ChangeAddress()
        {
            Address = "Zzyzx RdCalifornia, USA";
        }
	}
}

