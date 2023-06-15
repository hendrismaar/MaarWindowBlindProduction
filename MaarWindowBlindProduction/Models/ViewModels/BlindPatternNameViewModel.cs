namespace MaarWindowBlindProduction.Models.ViewModels
{
    public class BlindPatternNameViewModel
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<Pattern> PatternNames { get; set; }
    }
}
