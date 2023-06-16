using System.ComponentModel.DataAnnotations;

namespace MaarWindowBlindProduction.Models
{
    public class Pattern
    {
        [Range(1, 24)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
