using System.Net.Http;
using showTracker.BusinessLayer.Interfaces;

namespace showTracker.BusinessLayer.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClient HttpClient { get; set; } = new HttpClient();
    }
}
