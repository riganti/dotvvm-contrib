using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample3ViewModel : MasterViewModel
    {
        public string CrystalReportFile { get; set; } = "CrystalReports/PersonsCrystalReport.rpt";
    }
}

