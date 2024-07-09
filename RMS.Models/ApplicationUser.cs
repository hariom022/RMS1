using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? StoreId { get; set; }
        [ForeignKey("StoreId")]
        [ValidateNever]
        public StoreMaster Store { get; set; }

    }
}
