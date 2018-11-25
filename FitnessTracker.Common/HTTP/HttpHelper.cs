using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Common.HTTP
{
    public class HttpHelper
    {
        public static async Task<T> GetAsync<T>(string uri)
        {
            string responseJsonString = null;

            using (var httpClient = new HttpClient())
            {
                using (var r = httpClient.GetAsync(new Uri(uri)).Result)
                {
                    responseJsonString = await r.Content.ReadAsStringAsync();
                    r.EnsureSuccessStatusCode();
                    T result = JsonConvert.DeserializeObject<T>(responseJsonString);

                    return result;
                }
            }
        }

        public static async Task<T> PutAsync<T>(string uri, T obj)
        {
            string responseJsonString = null;

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(obj);

                using (var r = httpClient.PostAsync(new Uri(uri), new StringContent(content, Encoding.UTF8, "text/json")).Result)
                {
                    responseJsonString = await r.Content.ReadAsStringAsync();
                    r.EnsureSuccessStatusCode();
                    T result = JsonConvert.DeserializeObject<T>(responseJsonString);

                    return result;
                }
            }
        }
    }
}