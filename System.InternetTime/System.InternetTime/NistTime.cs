using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ModernHttpClient;

namespace System.InternetTime
{
    /// <summary>
    /// A helper to communitcate with the nist.time.gov server.
    /// </summary>
    public class NistTime : IInternetTime
    {
        public const string NistUrl = "http://nist.time.gov/actualtime.cgi?lzbc=siqm9b";
        public const string NistMediaTypeHeaderValue = "application/xhtml+xml";
        public static double NistReponseToMillisecondsFunction(string responseContent)
            => Convert.ToInt64(Regex.Match(responseContent, @"(?<=\btime="")[^""]*").Value)/1000.0; //regEx arg ex: //<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>

        /// <summary>
        /// Gets NIST time asynchronously. Can be awaited to avoid blocking UI due to latency. 
        /// </summary>
        /// <returns>The NIST time, in the NIST default time zone (GMT)</returns>
        public static async Task<DateTime?> GetTimeAsync() => await new NistTime().GetAsync();

        /// <summary>
        /// Gets the NIST time. Not asynchronous, could block UI if it encounters latency. 
        /// </summary>
        /// <returns>The NIST time, in the NIST default time zone (GMT)</returns>
        public static DateTime? GetTime() => GetTimeAsync().Result;

        /// <summary>
        /// Gets the time server URL.
        /// </summary>
        /// <value>
        /// The time server URL.
        /// </value>
        public string TimeServerUrl => NistUrl;

        /// <summary>
        /// Gets the media type header value for the WebRequest.
        /// </summary>
        /// <value>
        /// The media type header value.
        /// </value>
        public string MediaTypeHeaderValue => NistMediaTypeHeaderValue;

        /// <summary>
        /// Gets the response to milliseconds function, which you define to convert the string response from the time server into milliseconds.
        /// </summary>
        /// <value>
        /// The response to milliseconds function.
        /// </value>
        public Func<string, double> ResponseToMillisecondsFunc => NistReponseToMillisecondsFunction;

        /// <summary>
        /// Gets NIST time asynchronously. Can be awaited to avoid blocking UI due to latency. 
        /// </summary>
        /// <returns>The NIST time, in the NIST default time zone (GMT)</returns>
        public async Task<DateTime?> GetAsync() => await new Client(TimeServerUrl, MediaTypeHeaderValue, ResponseToMillisecondsFunc).GetAsync();

        /// <summary>
        /// Gets the NIST time. Not asynchronous, could block UI if it encounters latency. 
        /// </summary>
        /// <returns>The NIST time, in the NIST default time zone (GMT)</returns>
        public DateTime? Get() => GetAsync().Result;
    }
}