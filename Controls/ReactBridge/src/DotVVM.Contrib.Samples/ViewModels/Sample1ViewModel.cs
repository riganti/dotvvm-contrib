using System.Collections.Generic;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {
        public List<int> Data { get; set; } = new List<int> { 1, 5, 4, 3, 7, 2 };
        public List<string> Gradient { get; set; } = new List<string> { "#00c6ff", "#F0F", "#FF0" };
    }
}

