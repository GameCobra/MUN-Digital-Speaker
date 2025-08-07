using System.ComponentModel.DataAnnotations;

namespace MUN_Digital_Speaker.Models
{
    public class Delegation
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Times Spoken")]
        public int? TimesSpoken { get; set; }
        [Display(Name = "Has Requested To Speak")]
        public bool? RequestedToSpeak { get; set; }

    }
}
