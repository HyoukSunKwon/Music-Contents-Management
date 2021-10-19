using System;
using Assignment5.Models.ViewModels.Artist;

namespace Assignment5.Models.ViewModels.MediaItem
{
    public class MediaItemContentViewModel : MediaItemViewModel
    {
        

        public DateTime TimeStamp { get; set; }

        public ArtistBaseModel Artist { get; set; }
    }

    public class MediaItemViewModel : MediaItemBaseViewModel
    {
        public byte[] Content { get; set; }
    }
}