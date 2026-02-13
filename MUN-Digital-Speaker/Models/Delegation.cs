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
        [Display(Name = "Amendment Points")]
        public int? AmendmentPoints { get; set; }
        [Display(Name = "Has Requested To Speak")]
        public bool RequestedToSpeakGEN { get; set; }
        public bool RequestedToSpeakECO { get; set; }
        public bool RequestedToSpeakENV { get; set; }
        public bool RequestedToSpeakHE { get; set; }
        public bool RequestedToSpeakSEC { get; set; }


        public List<Amendment>? amendments { get; set; } 

    }
}
namespace MUN_Digital_Speaker.Models
{
    public class Amendment
    {
        public int ResolutionID { get; set; }
        public string? ClauseNumber { get; set; }
        public string? Metadata { get; set; }
        public required string Change { get; set; }
    }
}