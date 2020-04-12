using DotVVM.Contrib.Samples.Model;

namespace DotVVM.Contrib.Samples.Data
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