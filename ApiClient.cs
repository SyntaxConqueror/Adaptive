using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LR5
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; set; }
    }

    public class ApiClient
    {
       
        private HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }


        public async Task<ApiResponse<T>> Get<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    
                    JToken parsedJson = JToken.Parse(jsonString);
                
                    JArray resultsArray = (JArray)parsedJson["results"];

                    return new ApiResponse<T> { StatusCode = response.StatusCode, Data = resultsArray };
                }
                else
                {
                    return new ApiResponse<T> { StatusCode = response.StatusCode };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<T>> Post<T>(string url, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    
                    return new ApiResponse<T> { StatusCode = response.StatusCode, Data = jsonString };
                }
                else
                {
                    return new ApiResponse<T> { StatusCode = response.StatusCode };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { StatusCode = HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }
    }
}
