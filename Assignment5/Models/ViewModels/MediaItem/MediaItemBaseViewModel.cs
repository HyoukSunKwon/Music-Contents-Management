using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.MediaItem
{
    public class MediaItemBaseViewModel
    {
        public int Id { get; set; }

        public string StringId { get; set; }

        [Display(Name = "Descriptive Caption")]
        public string Caption { get; set; }

        public string ContentType { get; set; }
    }
}