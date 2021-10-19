using System.Collections.Generic;
using Assignment5.Models.ViewModels.MediaItem;

namespace Assignment5.Models.ViewModels.Artist
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {
        public ICollection<MediaItemViewModel> MediaItems { get; set; }
    }
}