using System.ComponentModel.DataAnnotations;
using Assignment5.Models.ViewModels.Artist;

namespace Assignment5.Models.ViewModels.MediaItem
{
    public class MediaItemAddFormViewModel : MediaItemBaseViewModel
    {
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }



        [Display(Name = "Media Item")]
        [DataType(DataType.Upload)]
        public string ContentInfo { get; set; }
    }
}