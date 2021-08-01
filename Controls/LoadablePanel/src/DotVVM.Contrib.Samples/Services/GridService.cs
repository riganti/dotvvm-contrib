using System;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Contrib.Samples.Pages.PagingRepeater;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.Services
{
    public class GridService
    {
        [AllowStaticCommand]
        public async Task<string> LoadItem(string id)
        {
            await Task.Delay(GetRandomDelay());
            return "black";
        }

        [AllowStaticCommand]
        public GridViewDataSet<ItemModel> FillGrid(GridViewDataSet<ItemModel> grid)
        {
            var itemCount = grid.PagingOptions.TotalItemsCount - grid.PagingOptions.PageSize * grid.PagingOptions.PageIndex;
            itemCount = Math.Min(grid.PagingOptions.PageSize, itemCount);

            grid.Items = Enumerable
                .Repeat(new ItemModel { Id = Guid.NewGuid().ToString(), Data = "waiting for data..." }, itemCount)
                .ToList();

            return grid;
        }

        private int GetRandomDelay()
        {
            return new Random((int)DateTime.Now.Ticks).Next(200, 5000);
        }

    }
}

