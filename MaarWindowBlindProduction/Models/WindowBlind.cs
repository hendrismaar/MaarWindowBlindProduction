using System.ComponentModel.DataAnnotations;

namespace MaarWindowBlindProduction.Models
{
    public class WindowBlind
    {
        //1, Jüri, Ratas, päris aadress, 11, true, true, true, false
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PatternNumber { get; set; } = 0;
        [UIHint("_manufacturingBools")]
        public bool ClothReady { get; set; }
        [UIHint("_manufacturingBools")]
        public bool FrameReady { get; set; }
        [UIHint("_packagingBools")]
        public bool ProductPackaged { get; set; }
        [UIHint("_deliveryBools")]
        public bool DeliveryStatus { get; set; } 
    }
}
