using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Contrib;
using DotVVM.Contrib.FAIcon;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {
        public FAIcons Icon { get; set; } = FAIcons.adn_brands;

        public bool ShowBuilding { get; set; } = false;
        
        public void Change()
        {
            Icon = FAIcons.adjust_solid;
        }
    }
}