using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.Services
{
    public class DataService
    {
        [AllowStaticCommand]
        public async Task<string> LoadData1()
        {
            await Task.Delay(2000);
            return "Loaded content in 2000 ms";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData2()
        {
            await Task.Delay(3000);
            return "Loaded content in 3000 ms";
        }
    }
}

