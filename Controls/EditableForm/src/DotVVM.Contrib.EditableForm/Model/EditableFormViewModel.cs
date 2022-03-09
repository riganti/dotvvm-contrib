using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.EditableForm.Model
{
    public class EditableFormViewModel<TData> : DotvvmViewModelBase, IEditableFormViewModel
    {
        private static readonly string EditableFormsOnPageKey = typeof(IEditableFormViewModel).FullName;

        private readonly Func<Task<TData>> loadAction;
        private readonly Func<TData, Task> saveAction;
        private bool needsRefresh;

        public TData Data { get; set; }

        public bool IsEditable { get; set; }

        public event Action Saved;
        public event Action EditCanceled;


        public EditableFormViewModel(Func<Task<TData>> loadAction, Func<TData, Task> saveAction)
        {
            this.loadAction = loadAction;
            this.saveAction = saveAction;
        }

        public override Task Load()
        {
            // register self in current request editable groups
            GetEditableFormsOnPage().Add(this);

            return base.Load();
        }

        public override async Task PreRender()
        {
            if (needsRefresh || !Context.IsPostBack)
            {
                Data = await loadAction();
                OnDataLoaded();
            }
            await base.PreRender();
        }

        public void Edit()
        {
            // allow only one group to be editable
            foreach (var form in GetEditableFormsOnPage())
            {
                if (form.IsEditable)
                {
                    form.Cancel();
                }
            }

            IsEditable = true;
            needsRefresh = true;
        }

        public async Task Save()
        {
            OnDataSaving();
            await saveAction(Data);
            OnDataSaved();

            IsEditable = false;
            needsRefresh = true;

            Saved?.Invoke();
        }

        public void Cancel()
        {
            IsEditable = false;
            needsRefresh = true;

            EditCanceled?.Invoke();
        }

        private List<IEditableFormViewModel> GetEditableFormsOnPage()
        {
            var forms = Context.HttpContext.GetItem<List<IEditableFormViewModel>>(EditableFormsOnPageKey);
            if (forms == null)
            {
                forms = new List<IEditableFormViewModel>();
                Context.HttpContext.SetItem(EditableFormsOnPageKey, forms);
            }
            return forms;
        }

        protected virtual void OnDataLoaded()
        {
        }

        protected virtual void OnDataSaving()
        {
        }

        protected virtual void OnDataSaved()
        {
        }

    }
}