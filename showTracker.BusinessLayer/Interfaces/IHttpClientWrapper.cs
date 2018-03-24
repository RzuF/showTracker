using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface IHttpClientWrapper
    {
        HttpClient HttpClient { get; set; }
    }
}
