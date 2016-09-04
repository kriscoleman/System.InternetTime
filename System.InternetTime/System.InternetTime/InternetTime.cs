using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ModernHttpClient;

namespace System.InternetTime
{
    public class InternetTime
    {
        public const string NistUrl = "http://nist.time.gov/actualtime.cgi?lzbc=siqm9b";
        const string MediaTypeHeaderValue = "application/xhtml+xml";



        public static async Task<DateTime?> GetNistTimeAsync()
        {
            var client = new HttpClient(new NativeMessageHandler {DisableCaching = true});
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(MediaTypeHeaderValue));
            var response = await client.GetAsync(NistUrl);
            if (!response.IsSuccessStatusCode) return null;

            var stream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync();
            var time = Regex.Match(content, @"(?<=\btime="")[^""]*").Value; //<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
            var milliseconds = Convert.ToInt64(time) / 1000.0;

            DateTime? dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            return dateTime;
        }

        public static DateTime? GetNistTime() => GetNistTimeAsync().Result;
    }
}