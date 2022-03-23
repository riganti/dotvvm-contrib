namespace DotVVM.Contrib.CkEditorMinimal.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {
        public string Text1 { get; set; } = "Sample text 1";
        public string Text2 { get; set; } = "Sample text 2";


        public void ChangeText1()
        {
            Text1 = "<p>The Suicide Squad by James Gunn is the best DC movie.</p>";
        }
    }
}

