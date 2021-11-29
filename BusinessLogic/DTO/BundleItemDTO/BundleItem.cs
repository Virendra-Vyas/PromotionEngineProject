using DataAccess.Enums;

namespace BusinessLogic.DTO.BundleItemDTO
{
    public class BundleItem
    {
        public int BundleCount { get; set; }
        public double PromotionDiscount { get; set; }
        public Discount DiscountType { get; set; }
        public double Amount { get; set; }
    }
}