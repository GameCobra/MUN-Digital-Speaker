namespace MUN_Digital_Speaker.Models
{
    public class SpeakerListControlStates
    {
        public bool allowSpeakRequests {get; set;}

        public int CurrentResolution { get; set; }

        public List<Resolution>? Resolutions { get; set; }

        public string firstSpeaker { get; set; } = "";
        public string secondSpeaker { get; set; } = "";
        public string thirdSpeaker { get; set; } = "";
    }
}
