using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DotVVM.Contrib.Samples
{
    public class DatabaseService
    {
        public DataTable Persons { get; set; }
        public DataTable Products { get; set; }

        public DatabaseService()
        {
            InitDatabase();
        }
        private void InitDatabase()
        {
            InitPersons();
            InitProducts();
        }

        private void InitProducts()
        {
            Products = new DataTable("Products");

            var dc0 = new DataColumn("Id", typeof(int));
            var dc1 = new DataColumn("Name", typeof(string));
            var dc2 = new DataColumn("Category", typeof(string));

            Products.Columns.Add(dc0);
            Products.Columns.Add(dc1);
            Products.Columns.Add(dc2);

            Products.Rows.Add(1, "Apple", "Fruit");
            Products.Rows.Add(2, "Banana", "Fruit");
            Products.Rows.Add(3, "Bread", "Food");
            Products.Rows.Add(4, "Beer", "Beverage");
        }

        private void InitPersons()
        {
            Persons = new DataTable("Persons");

            var dc0 = new DataColumn("Id", typeof(int));
            var dc1 = new DataColumn("FirstName", typeof(string));
            var dc2 = new DataColumn("Surname", typeof(string));
            var dc3 = new DataColumn("Email", typeof(string));

            Persons.Columns.Add(dc0);
            Persons.Columns.Add(dc1);
            Persons.Columns.Add(dc2);
            Persons.Columns.Add(dc3);


            Persons.Rows.Add(1, "Adam", "Smith", "a.smith123@gmail.com");
            Persons.Rows.Add(2, "John", "Johnson", "jj34@gmail.com");
            Persons.Rows.Add(3, "Mark", "Brown", "mark@email.com");
        }

        public DataTable GetTable(string crystalReportFile)
        {
            if (crystalReportFile.Contains("Persons"))
            {
                return Persons;
            }
            return Products;
        }
    }
}