namespace DotVVM.Contrib.MarkdownView.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
    {

        public string Markdown { get; set; } = @"
# Welcome to DotVVM!
* Item 1
* Item 2
* Item 3";

        public bool ConversionEnabled { get; set; }

    }
}

