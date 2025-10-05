using System.ComponentModel.DataAnnotations;

namespace MUN_Digital_Speaker.Models
{
    public class Delegation
    {
        public int Id { get; set; }
        public int Login { get; set; }
        public int Key { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Times Spoken")]
        public int? TimesSpoken { get; set; }
        [Display(Name = "Has Requested To Speak")]
        public bool? RequestedToSpeak { get; set; }

        //public 

    }
}

public class Amendment
{
    public int ResolutionID { get; set; }
    public  string? Metadata { get; set; }
    [Required]
    public string Change { get; set; }
}