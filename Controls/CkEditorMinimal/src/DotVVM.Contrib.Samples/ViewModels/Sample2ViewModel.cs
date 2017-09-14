using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {
        public string Text1 { get; set; } = "Test 1";
        public string Text2 { get; set; } = "Test 2";
    }
}

