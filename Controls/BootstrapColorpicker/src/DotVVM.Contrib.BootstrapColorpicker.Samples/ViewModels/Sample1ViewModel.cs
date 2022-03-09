namespace DotVVM.Contrib.BootstrapColorpicker.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {
        public string Color { get; set; } = "#aaaaaa";

        public void SetColor()
        {
            Color = "#bbbbbb";
        }
    }
}

