using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MaarWindowBlindProduction.Models
{
    public class Order
    {
        //1, Jüri, Ratas, 20230519202341, päris aadress, 11, true, true, true, false
        public int Id { get; set; }
        [Display(Name = "Order Id")]
        public string OrderId { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public Pattern Pattern { get; set; }
        [UIHint("_ManufacturingBools")]
        [Display(Name = "Blind status")]
        public bool ClothReady { get; set; }
        [UIHint("_ManufacturingBools")]
        [Display(Name = "Frame status")]
        public bool FrameReady { get; set; }
        [UIHint("_PackagingBools")]
        [Display(Name = "Product packaged")]
        public bool ProductPackaged { get; set; }
        [UIHint("_DeliveryBools")]
        [Display(Name = "Delivery status")]
        public bool DeliveryStatus { get; set; }
        public Order()
        {
            string dateTimeString = DateTime.Now.ToString();
            string cleanedDateTimeString = Regex.Replace(dateTimeString, @"[\s:./]", "");
            OrderId = cleanedDateTimeString.Replace("AM", "").Replace("PM", "");
        }
    }
}
