using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Select2.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{
	    public Select2ListOfStrings ListOfStrings { get; set; } = new Select2ListOfStrings();

	    public Select2ListOfObjects ListOfObjects { get; set; } = new Select2ListOfObjects();

    }

    public class Select2ListOfStrings : DotvvmViewModelBase
    {

        public List<string> CityNames { get; set; } = new List<string>() { "Prague", "New York", "Paris", "London" };

        public List<string> SelectedCityNames { get; set; } = new List<string>() { "Prague" };

        public string PreviousValues { get; set; }

        public void AddCity()
        {
            if (CityNames.Count == 4)
            {
                CityNames.Add("Berlin");
            }
        }

        public void Change()
        {
            SelectedCityNames = new List<string>() { "New York", "Paris" };
        }

        public void Submit()
        {

        }

        public override Task Load()
        {
            PreviousValues = string.Join(",", SelectedCityNames);
            return base.Load();
        }
    }

    public class Select2ListOfObjects : DotvvmViewModelBase
    {

        public List<CityData> Cities { get; set; } = new List<CityData>()
        {
            new CityData() { Name = "Prague", Id = 1 },
            new CityData() { Name = "New York", Id = 2 },
            new CityData() { Name = "Paris", Id = 3 },
            new CityData() { Name = "London", Id = 4 }
        };

        public List<int> SelectedCityIds { get; set; } = new List<int>() { 1 };

        public string PreviousValues { get; set; }

        public void AddCity()
        {
            if (Cities.Count == 4)
            {
                Cities.Add(new CityData() { Name = "Berlin", Id = 5 });
            }
        }

        public void Change()
        {
            SelectedCityIds = new List<int>() { 2, 3 };
        }

        public void Submit()
        {

        }

        public override Task Load()
        {
            PreviousValues = string.Join(",", SelectedCityIds.Select(i => Cities.Single(c => c.Id == i).Name));
            return base.Load();
        }
    }

    public class CityData
    {

        public string Name { get; set; }

        public int Id { get; set; }

    }
}

