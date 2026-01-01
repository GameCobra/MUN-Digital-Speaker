namespace MUN_Digital_Speaker.Models
{
    public class ResolutionDelegationViewModel
    {
        public required Delegation delegation { get; set; }
        public required List<Resolution> resolutions { get; set; }
    }
}
