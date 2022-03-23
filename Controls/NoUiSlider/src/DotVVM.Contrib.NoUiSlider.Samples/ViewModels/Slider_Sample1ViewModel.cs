namespace DotVVM.Contrib.NoUiSlider.Samples.ViewModels
{
	public class Slider_Sample1ViewModel : MasterViewModel
	{

	    public int SliderValue { get; set; } = 20;

	    public bool Enabled { get; set; } = true;

	    public void SetValue()
	    {
	        SliderValue = 50;
	    }
    }
}

