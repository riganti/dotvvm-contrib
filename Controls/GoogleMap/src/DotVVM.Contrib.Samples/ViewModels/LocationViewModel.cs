using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class LocationViewModel : MasterViewModel
    {
        public float Latitude { get; set; } = 43.5766682f;
        public float Longitude { get; set; } = -116.7723588f;


        public void ChangeLocation()
        {
            Latitude = 35.188263f;
            Longitude = -116.125422f;
        }
    }
}

