using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class PagingRepeaterViewModel : MasterViewModel
    {
        public GridViewDataSet<Item> Grid { get; set; } = new GridViewDataSet<Item>
        {
            PagingOptions = new PagingOptions
            {
                PageSize = 6,
                TotalItemsCount = 32
            }
        };

        [AllowStaticCommand]
        public async Task<string> LoadItem(string id)
        {
            await Task.Delay(GetRandomDelay());
            return "black";
        }

        [AllowStaticCommand]
        public GridViewDataSet<Item> FillGrid(GridViewDataSet<Item> grid)
        {
            grid.Items = Enumerable
                .Repeat(new Item { Id = "id", Data = "waiting for data..." }, grid.PagingOptions.PageSize)
                .ToList();

            return grid;
        }

        private int GetRandomDelay()
        {
            return new Random((int)DateTime.Now.Ticks).Next(200, 5000);
        }


        public override Task PreRender()
        {
            FillGrid(Grid);
            return base.PreRender();
        }

        public class Item
        {
            public string Id { get; set; }
            public string Data { get; set; }
        }
    }
}

