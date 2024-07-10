using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMS.Models;

namespace RMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<PurchaseOrderCart> PurchaseOrderCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<GoodsIssue> GoodsIssues { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ConsumptionEntry> ConsumptionEntries { get; set; }
        public DbSet<StoreStock> StoreStocks {  get; set; }
       public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Composite primary key
            modelBuilder.Entity<StoreStock>()
                .HasKey(d => new { d.ProductId, d.StoreId });

            // Configure the relationships
            modelBuilder.Entity<StoreStock>()
                .HasOne(d => d.Product)
                .WithMany(p => p.StoreStocks)
                .HasForeignKey(d => d.ProductId);

            modelBuilder.Entity<StoreStock>()
                .HasOne(d => d.Store)
                .WithMany(s => s.StoreStocks)
                .HasForeignKey(d => d.StoreId);



            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        ProductId = 1,
            //        ProductNumber = "P001",
            //        Description = "Dummy Product 1",
            //        Category = "Category A",
            //        DataSource = "Source 1",
            //        IsActive = true,
            //        CreatedBy = "A",

            //    },
            //    new Product
            //    {
            //        ProductId = 2,
            //        ProductNumber = "P002",
            //        Description = "Dummy Product 2",
            //        Category = "Category B",
            //        DataSource = "Source 2",
            //        IsActive = true,
            //        CreatedBy = "B",
            //    },
            //    new Product
            //    {
            //        ProductId = 3,
            //        ProductNumber = "P003",
            //        Description = "Dummy Product 3",
            //        Category = "Category C",
            //        DataSource = "Source 3",
            //        IsActive = true,
            //        CreatedBy = "C",
            //    },
            //    new Product
            //    {
            //        ProductId = 4,
            //        ProductNumber = "P004",
            //        Description = "Dummy Product 4",
            //        Category = "Category D",
            //        DataSource = "Source 2",
            //        IsActive = true,
            //        CreatedBy = "D",
            //    },
            //    new Product
            //    {
            //        ProductId = 5,
            //        ProductNumber = "P005",
            //        Description = "Dummy Product 5",
            //        Category = "Category E",
            //        DataSource = "Source 5",
            //        IsActive = true,
            //        CreatedBy = "E",
            //    }

            //    );

            //modelBuilder.Entity<Store>().HasData(
            //    new Store { StoreId = 1, StoreName = "Store1", Location = "Location 1", Manager="Manager A", DataSource="Data Source A" , IsActive=true, CreatedBy="Create A" },
            //    new Store { StoreId = 2, StoreName = "Store2", Location = "Location 2", Manager = "Manager B", DataSource = "Data Source B", IsActive = true, CreatedBy = "Create B" },
            //    new Store { StoreId = 3, StoreName = "Store3", Location = "Location 3", Manager = "Manager C", DataSource = "Data Source C", IsActive = true, CreatedBy = "Create C" }
            //    );








        }
    }
}
