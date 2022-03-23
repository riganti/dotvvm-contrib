using System;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.LoadablePanel.Samples.Services
{
    public class ItemService
    {
        [AllowStaticCommand]
        public async Task<string> LoadItem(string id)
        {
            await Task.Delay(GetRandomDelay());
            return "black";
        }

        private int GetRandomDelay()
        {
            return new Random((int)DateTime.Now.Ticks).Next(200, 5000);
        }
    }
}

