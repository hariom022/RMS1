using RMS.Models;
using RMS.Models.Dto;
using RMS.Models.ViewModels;
using RMS.Utility;
using RMSWeb.Services.IServices;

namespace RMSWeb.Services
{
	public class SapService : BaseService, ISapService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string inquiryUrl;
		public SapService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			inquiryUrl = configuration.GetValue<string>("ServiceUrls:SapAPI");
		}
		public Task<T> PostInquiryRequestAsync<T>(SalesInquiryRequestDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = inquiryUrl + "/RESTAdapter/SalesInquiryRequest"
			});
		}

		public Task<T> PostInquiryRequestAsync<T>(PurchaseOrderCartVM dto)
		{
			throw new NotImplementedException();
		}
	}
}
