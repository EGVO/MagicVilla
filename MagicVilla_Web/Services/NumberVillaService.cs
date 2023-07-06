using MagicVilla_utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class NumberVillaService : BaseService, INumberVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;

        public NumberVillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiveUrls:API_URL");
        }

        public Task<T> Create<T>(NumberVillaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = _villaUrl + "/api/v1/NumberVilla",
                Token = token
            });                
        }

        public Task<T> Delete<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = _villaUrl + "/api/v1/NumberVilla/" + id,
                Token = token
            });
        }

        public Task<T> Get<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = _villaUrl + "/api/v1/NumberVilla/" + id,
                Token = token
            });
        }

        public Task<T> GetAll<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = _villaUrl + "/api/v1/NumberVilla",
                Token = token
            });
        }

        public Task<T> Update<T>(NumberVillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = _villaUrl + "/api/v1/NumberVilla/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
