using System.Collections.Generic;

namespace DotVVM.Contrib.Samples.Model
{
    public class ChoiceQuestion : QuestionBase
    {

        public List<string> Choices { get; set; }

        public string Value { get; set; }

    }
}
