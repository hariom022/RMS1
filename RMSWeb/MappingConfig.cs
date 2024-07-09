using AutoMapper;
using RMS.Models;
using RMS.Models.Dto;

namespace RMSWeb
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Quotation, QuotationDTO>().ReverseMap();

            CreateMap<Quotation, QuotationCreateDTO>().ReverseMap();

            CreateMap<GoodsIssue, GoodsIssueCreateDTO>().ReverseMap();

            CreateMap<Invoice, InvoiceCreateDTO>().ReverseMap();
        }
    }
}
