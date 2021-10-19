using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Artist
{
    public class ArtistBaseViewModel : ArtistBaseModel
    {
        [Display(Name ="If Applicable, Artist's Birth Name")]
        [StringLength(50)]
        public string BirthName { get; set; }

        [Display(Name = "Birth or Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [StringLength(20)]
        public string Genre { get; set; }

        [Display(Name = "Artist Photo")]
        [Required, StringLength(200)]
        public string UrlArtist { get; set; }

        [DataType(DataType.MultilineText)]
        public string Portrayal { get; set; }
    }
}