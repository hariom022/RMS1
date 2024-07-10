using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Manager {  get; set; }
      
        public string? DataSource { get; set; }
   
        public bool? IsActive { get; set; }
      
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set;}

        //Nevigation Property
        public ICollection<StoreStock> StoreStocks { get; set; }

    }
}
