using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample3ViewModel : MasterViewModel
	{
        public IEnumerable<Item> Items { get; set; } = Enumerable.Repeat(new Item { Id = "id" } , 8);

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

        public class Item
        {
            public string Id { get; set; }
            public string Data { get; set; }
        }
    }
}

