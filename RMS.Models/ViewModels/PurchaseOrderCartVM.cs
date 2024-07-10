namespace RMS.Models.ViewModels
{
	public class PurchaseOrderCartVM
	{
		public IEnumerable<PurchaseOrderCart> PurchaseOrderCartList { get; set; }
		public PurchaseOrderHeader PurchaseOrderHeader { get; set; }
		public List<Product> Products { get; set; }
        public int SelectedProductId { get; set; }
        public int QuantityToAdd { get; set; }
    }
}
