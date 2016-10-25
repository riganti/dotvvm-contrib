using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class SwitchTestViewModel : DotvvmViewModelBase
	{
        public bool SwitchValue { get; set; }

        public bool Enabled { get; set; } = true;

    }
}

