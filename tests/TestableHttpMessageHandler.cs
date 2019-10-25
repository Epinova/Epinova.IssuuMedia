using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Epinova.IssuuMediaTests
{
    public class TestableHttpMessageHandler : HttpMessageHandler
    {
        private int _callCount;
        private Exception _expectedException;
        private HttpResponseMessage _expectedResponse;

        public List<Uri> CalledUrls { get; set; } = new List<Uri>();


        public int CallCount()
        {
            return _callCount;
        }

        public void GetAsyncReturns(HttpResponseMessage result)
        {
            _expectedResponse = result;
        }

        public void GetAsyncThrows(Exception exception)
        {
            _expectedException = exception;
        }

        public void SendAsyncReturns(HttpResponseMessage result)
        {
            _expectedResponse = result;
        }

        public void SendAsyncThrows(Exception exception)
        {
            _expectedException = exception;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CalledUrls.Add(request.RequestUri);
            _callCount++;
            if (_expectedException != null)
                throw _expectedException;

            return Task.FromResult(_expectedResponse);
        }
    }
}
