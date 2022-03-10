using System.Collections.Generic;

namespace DotVVM.Contrib.Select2.Samples.ViewModels
{
    public class Sample3ViewModel : MasterViewModel
    {
        public List<LetterItem> Letters { get; set; } = new List<LetterItem>() {
            new LetterItem{ Letter = "a", Id = 0 },
            new LetterItem{ Letter = "b", Id = 1 },
            new LetterItem{ Letter = "c", Id = 2 },
            new LetterItem{ Letter = "d", Id = 3 } };

        public int? SelectedLetter { get; set; }

        public void OnSelected()
        {
            NumberOfRequests++;
        }

        public int NumberOfRequests { get; set; }

        public void SetSelected()
        {
            SelectedLetter = 2;
        }
    }

    public class LetterItem
    {
        public int Id { get; set; }
        public string Letter { get; set; }
    }
}

