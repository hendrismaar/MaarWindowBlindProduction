using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaarWindowBlindProduction.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public int PatternId { get; set; }
        public List<SelectListItem>? Patterns { get; set; }
        public Order Order { get; set; }
    }
}
