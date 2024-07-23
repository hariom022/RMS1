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
        public string NPC_Code { get; set; }
        [Required]  
        public string? Number { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public string TypeDescription { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int GroupCode { get; set; }
        [Required]
        public string GroupDesc { get; set; }
        [Required]
        public string UoM { get; set; }
       
        //Nevigation Property
        public ICollection<StoreStock> StoreStocks { get; set; }


    }
}
