using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Contrib.LoadablePanel.Samples.Services;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib.LoadablePanel.Samples.Pages.PagingRepeater
{
    public class PagingRepeaterViewModel : MasterViewModel
    {
        private readonly GridService itemLoadingService;

        public GridViewDataSet<ItemModel> Grid { get; set; } = new GridViewDataSet<ItemModel>
        {
            PagingOptions = new PagingOptions
            {
                PageSize = 6,
                TotalItemsCount = 32
            }
        };

        public PagingRepeaterViewModel(GridService itemLoadingService)
        {
            this.itemLoadingService = itemLoadingService;
        }

        public List<string> LoadingItems { get; set; } = new List<string>();




        public override Task PreRender()
        {
            itemLoadingService.FillGrid(Grid);
            return base.PreRender();
        }
    }
}

