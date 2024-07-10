using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;

namespace RMS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IProductRepository Product { get; private set; }
        public IStoreRepository Store { get; private set; }
        public IPurchaseOrderCartRepository OrderCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPurchaseOrderHeaderRepository OrderHeader { get; private set; }
        public IPurchaseOrderItemRepository OrderItem { get; private set; }
        public IQuotationRepository Quotation { get; private set; }
        public IGoodsIssueRepository GoodsIssue { get; private set; }

        public IInvoiceRepository Invoice { get;private set; }
        public IProductStockRepository ProductStock { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            Store = new StoreRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderCart = new PurchaseOrderCartRepository(_db);
            Product = new ProductRepository(_db);
            OrderHeader = new PurchaseOrderHeaderRepository(_db);
            OrderItem = new PurchaseOrderItemRepository(_db);
            Quotation = new QuotationRepository(_db);
            GoodsIssue = new GoodsIssueRepository(_db);
            Invoice = new InvoiceRepository(_db);
            ProductStock = new ProductStockRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
