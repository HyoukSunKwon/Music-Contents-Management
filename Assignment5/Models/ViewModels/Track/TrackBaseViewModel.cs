using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackBaseViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track Genres")]
        [StringLength(20)]
        public string Genre { get; set; }

        [Display(Name = "Track name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Sample Clip")]
        public string ClipUrl
        {
            get
            {
                return $"/track/clip/{Id}";
            }
        }
    }
}