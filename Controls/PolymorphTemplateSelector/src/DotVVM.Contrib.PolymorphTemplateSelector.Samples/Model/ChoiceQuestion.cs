using System.Collections.Generic;

namespace DotVVM.Contrib.PolymorphTemplateSelector.Samples.Model
{
    public class ChoiceQuestion : QuestionBase
    {

        public List<string> Choices { get; set; }

        public string Value { get; set; }

    }
}
