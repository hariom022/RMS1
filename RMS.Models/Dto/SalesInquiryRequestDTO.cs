using System.ComponentModel;

namespace RMS.Models.Dto
{
    public class SalesInquiryRequestDTO
    {

        [DisplayName("EnquiryDocNo")]
        public Guid RequestNo { get; set; }

        [DisplayName("SoldToParty")]
        public string HF_Cust { get; set; }

        [DisplayName("CustRef")]
        public string? ReferenceNo { get; set; }

        [DisplayName("CustRefDate")]
        public string RefernceDate { get; set; } //Ref Date 

        [DisplayName("Item")]
        public List<SalesInquiryItemsDTO> salesInquiryItemsDTO { get; set; } = new List<SalesInquiryItemsDTO>();
    }

    public class SalesInquiryItemsDTO
    {
        [DisplayName("Material")]
        public string Number { get; set; }

        [DisplayName("ItemDesc")]
        public string Description { get; set; }

        public int OrderQuantity { get; set; }

        // Navigation property
        public int InquiryId { get; set; }
        public PurchaseOrderHeader? Inquiry { get; set; }

    }
}
