using System.Threading.Tasks;

namespace System.InternetTime
{
    /// <summary>
    /// A helper to communicate with a Web Time Server
    /// </summary>
    public interface IInternetTime
    {
        /// <summary>
        /// Gets the time server URL.
        /// </summary>
        /// <value>
        /// The time server URL.
        /// </value>
        string TimeServerUrl { get; }

        /// <summary>
        /// Gets the media type header value for the WebRequest.
        /// </summary>
        /// <value>
        /// The media type header value.
        /// </value>
        string MediaTypeHeaderValue { get; }

        /// <summary>
        /// Gets the response to milliseconds function, which you define to convert the string response from the time server into milliseconds.
        /// </summary>
        /// <value>
        /// The response to milliseconds function.
        /// </value>
        Func<string, double> ResponseToMillisecondsFunc { get; }

        /// <summary>
        /// Gets the Time asynchronously.
        /// </summary>
        /// <returns>The Time, in the Default Time Zone, or Null if server cannot be reached.</returns>
        Task<DateTime?> GetAsync();

        /// <summary>
        /// Gets this Time. Not asynchronous, so could block UI if it encounters latency.
        /// </summary>
        /// <returns></returns>
        DateTime? Get();
    }
}