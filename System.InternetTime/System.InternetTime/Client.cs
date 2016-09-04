using System.Threading.Tasks;

namespace System.InternetTime
{
    /// <summary>
    /// This is a agnostic Static Helper class to be used by IInternetTime implentations. Ensures they share some core functionality and aids in faking.
    /// </summary>
    public class Client
    {
        readonly string _url;
        readonly string _mediaTypeHeaderValue;
        readonly Func<string, double> _responseToMillisecondsFunc;

        /// <summary>
        /// Gets time asynchronously. Can be awaited to avoid blocking UI due to latency. 
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="mediaTypeHeaderValue">The media type header value.</param>
        /// <param name="responseToMillisecondsFunc">The response to milliseconds function.</param>
        /// <returns>The NIST time, in the NIST default time zone (GMT)</returns>
        public Client(string url, string mediaTypeHeaderValue,
            Func<string, double> responseToMillisecondsFunc)
        {
            _url = url;
            _mediaTypeHeaderValue = mediaTypeHeaderValue;
            _responseToMillisecondsFunc = responseToMillisecondsFunc;
        }

        /// <summary>
        /// Gets time asynchronously. Can be awaited to avoid blocking UI due to latency. 
        /// </summary>
        /// <returns>The NIST time, in the default time zone</returns>
        public virtual async Task<DateTime?> GetAsync()
        {
            var content = await SimpleHttpClient.GetAsync(_url, _mediaTypeHeaderValue);
            if (content == null) return null; //couldn't reach server

            var milliseconds = _responseToMillisecondsFunc(content);
            var dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return dateTime;
        }

        /// <summary>
        /// Gets time. Not asynchronous so may block UI due to latency. 
        /// </summary>
        /// <returns>The NIST time, in the default time zone</returns>
        public DateTime? Get() => GetAsync().Result;

        /// <summary>
        /// Converts the milliseconds to date time.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        public virtual DateTime ConvertMillisecondsToDateTime(double milliseconds) => new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
    }
}