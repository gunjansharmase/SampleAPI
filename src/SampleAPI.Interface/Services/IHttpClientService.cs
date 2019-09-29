using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SampleAPI.Interface.Services
{
    public interface IHttpClientService
    {
        HttpClient Client { get; }
    }
}
