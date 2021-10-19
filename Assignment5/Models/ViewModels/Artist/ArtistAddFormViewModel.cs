using System.Web.Mvc;

namespace Assignment5.Models.ViewModels.Artist
{
    public class ArtistAddFormViewModel : ArtistBaseViewModel
    {
        public SelectList GenreList { get; set; }
    }
}