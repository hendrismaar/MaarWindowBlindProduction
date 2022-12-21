using System.ComponentModel.DataAnnotations;

namespace MaarWindowBlindProduction.Models.ViewModels
{
    public class WindowBlindViewModel
    {
        public int Id { get; set; }
        [UIHint("_ManufacturingBools")]
        public bool ClothReady { get; set; }
        [UIHint("_ManufacturingBools")]
        public bool FrameReady { get; set; }
        [UIHint("_PackagingBools")]
        public bool ProductPackaged { get; set; }
        [UIHint("_DeliveryBools")]
        public bool DeliveryStatus { get; set; }
    }
}
