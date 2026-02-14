namespace MUN_Digital_Speaker.Models
{
    public class SpeakerListControlStates
    {
        public bool allowSpeakRequestsGEN {get; set;}
        public bool allowSpeakRequestsECO { get; set; }
        public bool allowSpeakRequestsENV { get; set; }
        public bool allowSpeakRequestsHE { get; set; }
        public bool allowSpeakRequestsSEC { get; set; }


        public int CurrentResolutionGEN { get; set; }
        public int CurrentResolutionECO { get; set; }
        public int CurrentResolutionENV { get; set; }
        public int CurrentResolutionHE { get; set; }
        public int CurrentResolutionSEC { get; set; }


        public List<Resolution>? Resolutions { get; set; }

        public List<string> topSpeakerListGEN { get; set; } = new List<string>();
        public List<string> topSpeakerListECO { get; set; } = new List<string>();
        public List<string> topSpeakerListENV { get; set; } = new List<string>();
        public List<string> topSpeakerListHE { get; set; } = new List<string>();
        public List<string> topSpeakerListSEC { get; set; } = new List<string>();


        //Constants





    }
}
