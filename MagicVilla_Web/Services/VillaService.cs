﻿using MagicVilla_utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiveUrls:API_URL");
        }

        public Task<T> Create<T>(VillaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = _villaUrl + "/api/v1/Villa",
                Token = token
            });                
        }

        public Task<T> Delete<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = _villaUrl + "/api/v1/Villa/" + id,
                Token = token
            });
        }

        public Task<T> Get<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = _villaUrl + "/api/v1/Villa/" + id,
                Token = token
            });
        }

        public Task<T> GetAll<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = _villaUrl + "/api/v1/Villa",
                Token = token
            });
        }
        
        public Task<T> GetAllPaged<T>(string token, int pageNumber, int pageSize)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = _villaUrl + "/api/v1/Villa/VillasPaged",
                Token = token,
                Parameters = new Parameters() { PageNumber = pageNumber, PageSize = pageSize }
            });
        }

        public Task<T> Update<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = _villaUrl + "/api/v1/Villa/" + dto.Id,
                Token = token
            });
        }
    }
}
