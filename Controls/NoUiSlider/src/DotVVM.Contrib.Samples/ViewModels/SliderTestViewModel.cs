using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class SliderTestViewModel : DotvvmViewModelBase
    {

        public string Title { get; set; }

        public int SliderValue { get; set; } = 20;

        public bool Enabled { get; set; } = true;

        public SliderTestViewModel()
        {
            Title = "Hello from DotVVM!";
        }

    }
}
