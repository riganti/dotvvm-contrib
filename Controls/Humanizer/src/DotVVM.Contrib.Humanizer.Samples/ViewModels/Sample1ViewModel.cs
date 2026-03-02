using System;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Humanizer.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {
        public DateTime? DateTimeValue { get; set; } = DateTime.Now.AddHours(-2);
        public DateTime? NullValue { get; set; }
        public DateTime? FutureValue { get; set; } = DateTime.Now.AddDays(3);
        public TimeSpan TimeSpanValue { get; set; } = TimeSpan.FromMinutes(90);
        public TimeSpan NegativeTimeSpan { get; set; } = TimeSpan.FromSeconds(-45);

        public void SetToNow()
        {
            DateTimeValue = DateTime.Now;
        }

        public void SetToYesterday()
        {
            DateTimeValue = DateTime.Now.AddDays(-1);
        }

        public void SetToLastWeek()
        {
            DateTimeValue = DateTime.Now.AddDays(-7);
        }
    }
}
