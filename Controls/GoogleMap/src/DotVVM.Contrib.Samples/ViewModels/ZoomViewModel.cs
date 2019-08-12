using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class ZoomViewModel : MasterViewModel
    {
        public int Zoom { get; set; } = 20;
        public void ChangeZoom()
        {
            Zoom = 2;
        }
    }
}

