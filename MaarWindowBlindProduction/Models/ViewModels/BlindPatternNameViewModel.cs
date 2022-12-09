namespace MaarWindowBlindProduction.Models.ViewModels
{
    public class BlindPatternNameViewModel
    {
        public ICollection<WindowBlind> WindowBlinds { get; set; }
        public ICollection<PatternName> PatternNames { get; set; }
    }
}
