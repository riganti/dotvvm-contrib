namespace DotVVM.Contrib.MonacoEditor.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{
        public string EditableCode { get; set; } = "const greeting = 'Hello, Monaco!';";

        public string ReadOnlyCode { get; set; } = "This editor is read-only.";

        public string OriginalCode { get; set; } = "const answer = 41;";

        public string ModifiedCode { get; set; } = "const answer = 42;";
	}
}
