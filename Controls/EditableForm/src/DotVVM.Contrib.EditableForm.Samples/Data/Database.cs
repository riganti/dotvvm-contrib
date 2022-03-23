using DotVVM.Contrib.EditableForm.Samples.Model;

namespace DotVVM.Contrib.EditableForm.Samples.Data
{
    public class Database
    {

        public static CustomerDTO CustomerInstance { get; set; } = new CustomerDTO()
        {
            FirstName = "Mr.",
            LastName = "DotVVM",
            IsVip = true
        };

    }
}