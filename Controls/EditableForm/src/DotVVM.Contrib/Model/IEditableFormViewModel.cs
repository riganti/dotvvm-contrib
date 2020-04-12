using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotVVM.Contrib.Model
{
    public interface IEditableFormViewModel
    {
        bool IsEditable { get; }

        void Edit();

        void Cancel();

        Task Save();
    }
}
