using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Contrib.Model;
using DotVVM.Contrib.Samples.Data;
using DotVVM.Contrib.Samples.Model;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{

        public EditableFormViewModel<CustomerDTO> CustomerForm { get; set; }

        public Sample1ViewModel()
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

