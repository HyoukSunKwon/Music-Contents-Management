using System.Web;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackClipEditViewModel : TrackBaseViewModel
    {
        public HttpPostedFileBase SampleClip { get; set; }
    }
}