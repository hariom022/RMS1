using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    /// <summary>
    /// Details of products
    /// </summary>
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? Number { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string? Category { get; set; }

       
        public string? DataSource { get; set; }
    
        public bool? IsActive { get; set; }
      
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }  
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }  
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; } 

        //Nevigation Property
        public ICollection<StoreStock> StoreStocks { get; set; }


    }
}
