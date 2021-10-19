using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models.ViewModels.Track
{
    public class TrackClipEditFormViewModel : TrackBaseViewModel
    {
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string SampleClip { get; set; }
    }
}