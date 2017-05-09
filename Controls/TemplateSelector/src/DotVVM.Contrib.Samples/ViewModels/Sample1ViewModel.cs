using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {

        public List<ItemData> Items { get; set; } = new List<ItemData>();


        public void Add(string type)
        {
            Items.Add(new ItemData() { Type = type });
        }

        public void Remove(ItemData item)
        {
            Items.Remove(item);
        }

        public void MoveUp(ItemData item)
        {
            var index = Items.IndexOf(item);
            if (index > 0)
            {
                var tmp = Items[index];
                Items[index] = Items[index - 1];
                Items[index - 1] = tmp;
            }
        }

        public void MoveDown(ItemData item)
        {
            var index = Items.IndexOf(item);
            if (index < Items.Count - 1)
            {
                var tmp = Items[index];
                Items[index] = Items[index + 1];
                Items[index + 1] = tmp;
            }
        }
    }

    public class ItemData
    {

        public string Type { get; set; }

        public string Text { get; set; }

        public string Text2 { get; set; }

        public string Url { get; set; }

    }
}

