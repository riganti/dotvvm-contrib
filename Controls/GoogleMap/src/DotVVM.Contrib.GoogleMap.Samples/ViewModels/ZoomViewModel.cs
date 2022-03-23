namespace DotVVM.Contrib.GoogleMap.Samples.ViewModels
{
    public class ZoomViewModel : MasterViewModel
    {
        public int Zoom { get; set; } = 20;
        public void ChangeZoom()
        {
            Zoom = 2;
        }
    }
}

