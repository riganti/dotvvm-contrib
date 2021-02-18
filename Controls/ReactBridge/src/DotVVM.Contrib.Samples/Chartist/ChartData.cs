using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace DotVVM.Contrib.Samples.Chartist
{
    public class ChartData
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<string> labels { get; set; }
        [Bind(Direction.None)]
        public IEnumerable<string> Labels { get => labels; set => labels = value; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IList<IEnumerable<double>> series { get; set; }
        [Bind(Direction.None)]
        public IList<IEnumerable<double>> Series { get => series; set => series = value; }
    }
}

