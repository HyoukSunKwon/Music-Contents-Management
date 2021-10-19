using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Assignment5.Models.ViewModels.Album;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackAddFormViewModel : TrackBaseViewModel
    {
        public SelectList GenreList { get; set; }

        public AlbumBaseModel Album { get; set; }

        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string SampleClip { get; set; }
    }
}