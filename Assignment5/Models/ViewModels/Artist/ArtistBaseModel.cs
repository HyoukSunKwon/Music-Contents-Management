using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Artist
{
    public class ArtistBaseModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }
    }
}