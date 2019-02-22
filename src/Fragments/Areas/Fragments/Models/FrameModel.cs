using System.ComponentModel.DataAnnotations;

namespace Fragments.Areas.Fragments.Models
{
    public class FrameModel
    {
        [Required]
        public string Name { get; set; }
        [RelativeUrl]
        public string[] Css { get; set; }
        [RelativeUrl]
        public string[] Html { get; set; }
        [RelativeUrl]
        public string[] Js { get; set; }
    }
}