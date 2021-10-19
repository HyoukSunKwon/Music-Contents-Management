using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Album
{
    public class AlbumBaseModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }
    }
}