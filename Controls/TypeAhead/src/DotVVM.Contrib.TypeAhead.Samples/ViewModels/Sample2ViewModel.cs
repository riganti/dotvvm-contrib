using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.TypeAhead.Samples.ViewModels
{
    public class Sample2ViewModel : DotvvmViewModelBase
    {

        public List<string> Items { get; set; } = new List<string>()
        {
            "A1",
            "A2",
            "A3",
            "B1",
            "B2",
            "B3"
        };

        public string SelectedItem { get; set; }

        public string SelectedItem2 { get; set; }

        public int Changes { get; set; }

        public int Changes2 { get; set; }

        public void SelectionChanged()
        {
            Changes++;
        }

        public void SelectionChanged2()
        {
            Changes2++;
        }
    }
}

