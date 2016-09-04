using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModernHttpClient;

namespace System.InternetTime
{
    /// <summary>
    /// Extract this into another new library, replace Modern HTTP Client with this
    /// </summary>
    public class SimpleHttpClient
    {
        public static async Task<string> GetAsync(string url, string mediaTypeHeaderValue)
        {
            var client = new HttpClient(new NativeMessageHandler { DisableCaching = true });
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(mediaTypeHeaderValue));
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var stream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }
    }
}