using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.ViewModels
{
	public class Sample1ViewModel : MasterViewModel
	{

	    public List<string> CountryNames { get; set; } = new List<string>()
	    {
	        "Czech Republic",
	        "Germany",
	        "Poland",
	        "Austria",
	        "Slovakia"
	    };

	    public string SelectedCountryName { get; set; } = "Germany";



	    public List<CountryData> Countries { get; set; } = new List<CountryData>()
	    {
	        new CountryData() { Id = 1, Name = "Czech Republic" },
		new CountryData() { Id = 2, Name = "Germany" },
		new CountryData() { Id = 3, Name = "Poland" },
		new CountryData() { Id = 4, Name = "Austria" },
		new CountryData() { Id = 5, Name = "Slovakia" }
	    };

	    public int? SelectedCountry { get; set; }

	    public int? SelectedCountryId { get; set; } = 3;



	    public void AddCountry()
	    {
	        var name = "Country " + Countries.Count;

	        CountryNames.Add(name);
	        Countries.Add(new CountryData() { Id = Countries.Count + 1, Name = name });
	    }

	    public void ChangeCountryName()
	    {
	        SelectedCountryName = CountryNames[CountryNames.Count - 1];
	    }

	    public void ChangeCountryId()
	    {
	        SelectedCountryId = Countries[Countries.Count - 1].Id;
	    }
    }

    public class CountryData
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

