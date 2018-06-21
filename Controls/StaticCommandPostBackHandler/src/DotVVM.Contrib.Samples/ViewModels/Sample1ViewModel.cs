using System.Threading.Tasks;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
	{
        public string Message { get; set; }

        public async Task Action(int seconds)
        {
            await Task.Delay(seconds * 1000);
        }
	}
}

