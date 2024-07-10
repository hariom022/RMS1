namespace RMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        IStoreRepository Store { get; }
        IPurchaseOrderCartRepository OrderCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IPurchaseOrderHeaderRepository OrderHeader { get; }
        IPurchaseOrderItemRepository OrderItem { get; }
        IQuotationRepository Quotation { get; }
        IGoodsIssueRepository GoodsIssue { get; }
        IInvoiceRepository Invoice { get; } 
        IProductStockRepository ProductStock { get; }

        void Save();
    }
}
