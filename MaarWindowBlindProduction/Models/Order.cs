using System.ComponentModel.DataAnnotations;

namespace MaarWindowBlindProduction.Models
{
    public class Order
    {
        //1, Jüri, Ratas, päris aadress, 11, true, true, true, false
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PatternNumber { get; set; } = 1;
        [UIHint("_ManufacturingBools")]
        public bool ClothReady { get; set; }
        [UIHint("_ManufacturingBools")]
        public bool FrameReady { get; set; }
        [UIHint("_PackagingBools")]
        public bool ProductPackaged { get; set; }
        [UIHint("_DeliveryBools")]
        public bool DeliveryStatus { get; set; }
        public Order()
        {
            Guid = Guid.NewGuid();
        }
    }
}
