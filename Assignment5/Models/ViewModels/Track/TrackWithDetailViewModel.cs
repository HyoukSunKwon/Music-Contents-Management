using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment5.Models.ViewModels.Album;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name = "Number of albums with this track")]
        public int NumberOfAlbums => Albums.Count;

        [Display(Name = "Albums with this track")]
        public ICollection<AlbumBaseViewModel> Albums { get; set; }
    }
}