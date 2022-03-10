using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Contrib.PolymorphTemplateSelector.Samples.Model;

namespace DotVVM.Contrib.PolymorphTemplateSelector.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{

        public List<QuestionEntry> Questions { get; set; }

        public string LastMessage { get; set; }

        public override Task Init()
        {
            if (!Context.IsPostBack)
            {
                Questions = new List<QuestionEntry>();
                AddYesNo();
                AddNumber();
                AddChoice();
                AddOpenText();
                AddEmpty();
            }

            return base.Init();
        }

        public void AddYesNo()
        {
            AddEntry(new QuestionEntry()
            {
                YesNo = new YesNoQuestion()
                {
                    Question = "Do you like DotVVM?",
                    Value = true
                }
            });
        }

        public void AddChoice()
        {
            AddEntry(new QuestionEntry()
            {
                Choice = new ChoiceQuestion()
                {
                    Question = "Where did you learn about DotVVM?",
                    Choices = new List<string>() { "Google", "Conference", "Blog", "Other" }
                }
            });
        }

        public void AddNumber()
        {
            AddEntry(new QuestionEntry()
            {
                Number = new NumberQuestion()
                {
                    Question = "How satisfied are you with DotVVM?",
                    Min = 1,
                    Max = 10,
                    Value = 10
                }
            });
        }

        public void AddOpenText()
        {
            AddEntry(new QuestionEntry()
            {
                OpenText = new OpenTextQuestion()
                {
                    Question = "Other comments..."
                }
            });
        }

        public void AddEmpty()
        {
            AddEntry(new QuestionEntry()
            {
            });
        }

        public void MoveUp(int index)
        {
            var tmp = Questions[index - 1];
            Questions[index - 1] = Questions[index];
            Questions[index] = tmp;
        }

        public void MoveDown(int index)
        {
            var tmp = Questions[index + 1];
            Questions[index + 1] = Questions[index];
            Questions[index] = tmp;
        }

        public void Delete(int index)
        {
            Questions.RemoveAt(index);
        }

        private void AddEntry(QuestionEntry questionEntry)
        {
            questionEntry.Id = (Questions.Max(q => (int?)q.Id) ?? 0) + 1;
            Questions.Add(questionEntry);
        }


        public void TestPostback(string message)
        {
            LastMessage = message;
        }
    }
}

