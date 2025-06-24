using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace Soft {

    public static class WebService<T>
    {

        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<T?> GetDataAsync(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        // embryon d'idee, abandonne (on n'a pas le temps)
        //public interface WebResponse
        //{
        //    public Dictionary<string, string> GetHeaders();
        //    public string? GetBody();
        //}

    }


    public static class IPInfo
	{

        public class IPServiceGeoDataResponse
        {
            public string ip { get; set; }
            // etc ... region, country, ...
        }

        public static string ServiceURL => "https://ipinfo.io/";
		public static string EndpointPath_GeoData => "/geo";

		public static string GetURL_GeoData(string ip)
		{
			return $"{ServiceURL}{ip}{EndpointPath_GeoData}";
		}

        public static Task<IPServiceGeoDataResponse?> GetGeoDataAsync(string ip)
        {
            string url = GetURL_GeoData(ip);
            return WebService<IPServiceGeoDataResponse>.GetDataAsync(url);
        }


    }

}