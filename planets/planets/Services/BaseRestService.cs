using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace planets.Services
{
    /// <summary>
    /// The base rest service. Any rest service should inherit from this.
    /// </summary>
    public class BaseRestService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A HttpResponseMessage object.</returns>
        public Task<HttpResponseMessage> SendRequest(HttpMethod method, string url)
        {
            return this.SendRequest(method, new Uri(url), null);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A HttpResponseMessage object.</returns>
        public Task<HttpResponseMessage> SendRequest(HttpMethod method, Uri url)
        {
            return this.SendRequest(method, url, null);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>A HttpResponseMessage object.</returns>
        public Task<HttpResponseMessage> SendRequest(HttpMethod method, string url, StringContent content)
        {
            return this.SendRequest(method, new Uri(url), content);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>A HttpResponseMessage object.</returns>
        public async Task<HttpResponseMessage> SendRequest(HttpMethod method, Uri url, StringContent content)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            if (content != null)
            {
                requestMessage.Content = content;
            }

            requestMessage.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb250ZXN0YW50X2lkIjoiZGgteGFtYXJpbiIsImlhdCI6MTU4NDA1ODUzMSwiZXhwIjoxNTg0MDY5MzMxfQ.enTw2QCQcDhev4kQh5RGYA_pO014kriT_-KUI2SsT8s");

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }

            return await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
        }
    }
}
