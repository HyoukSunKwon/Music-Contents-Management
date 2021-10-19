using System.ComponentModel.DataAnnotations;
using System.Web;
using Assignment5.Models.ViewModels.Artist;

namespace Assignment5.Models.ViewModels.MediaItem
{
    public class MediaItemAddViewModel : MediaItemBaseViewModel
    {
        public int ArtistId { get; set; }

        [Required]
        public HttpPostedFileBase ContentInfo { get; set; }
    }
}