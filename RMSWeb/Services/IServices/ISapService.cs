using RMS.Models.Dto;
using RMS.Models.ViewModels;

namespace RMSWeb.Services.IServices
{
    public interface ISapService
    {
        //All the POST request to SAP
        Task<T> PostInquiryRequestAsync<T>(SalesInquiryRequestDTO dto);
        Task<T> PostInquiryRequestAsync<T>(PurchaseOrderCartVM dto);


    }
}
