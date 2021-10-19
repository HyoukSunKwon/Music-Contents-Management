using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Album
{
    public class AlbumBaseViewModel : AlbumBaseModel
    {
        [StringLength(20),Required]
        [Display(Name = "Album Primary Genre")]
        public string Genre { get; set; }
        
        [StringLength(255)]
        [Display(Name = "Album Image")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Depiction { get; set; }
    }
}