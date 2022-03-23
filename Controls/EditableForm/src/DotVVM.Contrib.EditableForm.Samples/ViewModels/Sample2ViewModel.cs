using System.Threading.Tasks;
using DotVVM.Contrib.EditableForm.Model;
using DotVVM.Contrib.EditableForm.Samples.Data;
using DotVVM.Contrib.EditableForm.Samples.Model;

namespace DotVVM.Contrib.EditableForm.Samples.ViewModels
{
    public class Sample2ViewModel : MasterViewModel
    {

        public EditableFormViewModel<CustomerDTO> CustomerForm { get; set; }

        public Sample2ViewModel()
        {
            CustomerForm = new EditableFormViewModel<CustomerDTO>(LoadCustomerAsync, SaveCustomerAsync);
        }

        private Task<CustomerDTO> LoadCustomerAsync()
        {
            // pretend that we are loading data from a database
            var customer = Database.CustomerInstance;

            return Task.FromResult(customer);
        }

        private Task SaveCustomerAsync(CustomerDTO customer)
        {
            // pretend that we are storing data in a database
            Database.CustomerInstance = customer;

            return Task.CompletedTask;
        }
    }
}
