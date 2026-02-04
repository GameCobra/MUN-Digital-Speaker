namespace MUN_Digital_Speaker.Models
{
    public class SpeakerListControlStates
    {
        public bool allowSpeakRequests {get; set;}

        public int CurrentResolution { get; set; }

        public List<Resolution>? Resolutions { get; set; }
    }
}
