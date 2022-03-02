using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {
        public string Text1 { get; set; } = "Sample text 1";
        public string Text2 { get; set; } = "Sample text 2";


        public void ChangeText1()
        {
            Text1 = "<p>Changed text 1</p>";
        }
    }
}

