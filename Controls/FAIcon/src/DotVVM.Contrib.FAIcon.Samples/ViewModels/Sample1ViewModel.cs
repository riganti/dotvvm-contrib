namespace DotVVM.Contrib.FAIcon.Samples.ViewModels
{
    public class Sample1ViewModel : MasterViewModel
    {
        public FAIcons Icon { get; set; } = FAIcons.adn_brands;

        public bool ShowBuilding { get; set; } = false;
        
        public void Change()
        {
            Icon = FAIcons.adjust_solid;
        }
    }
}