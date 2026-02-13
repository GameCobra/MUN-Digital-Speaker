namespace MUN_Digital_Speaker.Models
{
    public class SpeakRequest
    {
        public int Login { get; set; }
        public bool IsRevoking { get; set; }
        public required string Councel { get; set; }
    }
}
