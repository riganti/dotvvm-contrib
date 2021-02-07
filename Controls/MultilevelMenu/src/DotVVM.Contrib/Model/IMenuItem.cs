using System.Collections.Generic;

namespace DotVVM.Contrib.Model
{
    public interface IMenuItem
    {
        string Text { get; }

        string NavigateUrl { get; }

        bool IsActive { get; }

        IEnumerable<IMenuItem> ChildItems { get; }
    }
}