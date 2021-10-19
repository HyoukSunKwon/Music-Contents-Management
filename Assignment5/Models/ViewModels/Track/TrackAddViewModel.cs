using System.Web;
using System.Web.Mvc;
using Assignment5.Models.ViewModels.Album;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackAddViewModel : TrackBaseViewModel
    {
        public SelectList GenreList { get; set; }
        public HttpPostedFileBase SampleClip { get; set; }

        public AlbumBaseModel Album { get; set; }
    }
}