using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MaarWindowBlindProduction.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        [Display(Name = "Pattern")]
        public int PatternId { get; set; }
        public List<SelectListItem>? Patterns { get; set; }
        public Order Order { get; set; }
    }
}
