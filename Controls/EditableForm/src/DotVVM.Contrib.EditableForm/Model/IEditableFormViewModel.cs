using System.Threading.Tasks;

namespace DotVVM.Contrib.EditableForm.Model
{
    public interface IEditableFormViewModel
    {
        bool IsEditable { get; }

        void Edit();

        void Cancel();

        Task Save();
    }
}
