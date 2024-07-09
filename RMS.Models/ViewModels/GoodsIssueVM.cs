using RMS.Models.Dto;

namespace RMS.Models.ViewModels
{
    public class GoodsIssueVM
    {
        public GoodsIssue GoodsIssue { get; set; }

        public IEnumerable<GoodsIssueProductDTO> GoodsIssueProductList { get; set; }

    }
}
