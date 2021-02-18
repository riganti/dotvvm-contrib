using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Contrib.Samples.Chartist;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {
        public ChartData MultipleSeries1ChartData { get; set; } = new ChartData
        {
            Labels = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" },
            Series = new List<IEnumerable<double>>
            {
                new []{ 12.0, 9.0, 7.0, 8.0, 5.0 },
                new []{ 2.0, 1.0, 3.5, 7.0, 3.0 },
                new []{ 1.0, 3.0, 4.0, 5.0, 6.0 }

            }
        };

        public ChartData MultipleSeries2ChartData { get; set; } = new ChartData
        {
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8" },
            Series = new List<IEnumerable<double>>
            {
                new []{ 1.0, 2, 3, 1, -2, 0, 1, 0 },
                new []{-2, -1, -2, -1, -2.5, -1, -2, -1 },
                new []{ 0.0, 0, 0, 1, 2, 2.5, 2, 1 },
                new []{ 2.5, 2, 1, 0.5, 1, 0.5, -1, -2.5 }

            }
        };

        public ChartData SingleSeriesChartData { get; set; } = new ChartData
        {
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8" },
            Series = new List<IEnumerable<double>>
            {
                new [] { 5.0, 9, 7, 8, 5, 3, 5, 4 }
            }
        };

        public ChartData SingleSeriesChartDataWithoutLabels { get; set; } = new ChartData
        {
            Labels = new List<string> { "Monday", "Tuesday", "Wednesday" },

            Series = new List<IEnumerable<double>>
            {
                new [] { 5.0, 3, 4 }
            }
        };

        public ChartData BarChartData { get; set; } = new ChartData
        {
            Labels = new List<string> { "First quarter of the year", "Second quarter of the year", "Third quarter of the year", "Fourth quarter of the year" },
            Series = new List<IEnumerable<double>>
            {
                new []{ 60000.0, 40000, 80000, 70000 },
                new []{ 40000.0, 30000, 70000, 65000 },
                new []{ 8000.0, 3000, 10000, 6000 }

            }
        };
    }
}

