namespace DotVVM.Contrib.NoUiSlider.Samples.ViewModels
{
	public class Switch_Sample1ViewModel : MasterViewModel
	{

	    public bool SwitchValue { get; set; }

	    public bool Enabled { get; set; } = true;

	    public void SetValue()
	    {
	        SwitchValue = !SwitchValue;
	    }
    }
}

