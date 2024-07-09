namespace RMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProductMasterRepository Product { get; }
        IStoreMasterRepository Store { get; }
        IPurchaseOrderCartRepository OrderCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IPurchaseOrderHeaderRepository OrderHeader { get; }
        IPurchaseOrderItemRepository OrderItem { get; }
        IQuotationRepository Quotation { get; }
        IGoodsIssueRepository GoodsIssue { get; }
        IInvoiceRepository Invoice { get; } 
        IProductStockMasterRepository ProductStock { get; }

        void Save();
    }
}
