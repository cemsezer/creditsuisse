using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Qis.App.Tests
{
    /// <summary>
    /// Helper class to enable mocking http requests and responses
    /// </summary>
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("This is meant to be mocked in a unit test");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
