using System.Web.Mvc;
using Assignment5.Models.ViewModels.Artist;

namespace Assignment5.Models.ViewModels.Album
{
    public class AlbumAddFormViewModel : AlbumBaseViewModel
    {
        public SelectList GenreList { get; set; }


        public ArtistBaseModel Artist { get; set; }
    }
}