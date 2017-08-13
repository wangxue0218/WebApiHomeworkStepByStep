using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyApplication
{
    public class LogHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (IMyLogger)request.GetDependencyScope().GetService(typeof(IMyLogger));
            DateTime requestStart = DateTime.UtcNow;
            logger.Info("Record start time for request.");
            return base.SendAsync(request, cancellationToken).ContinueWith(
                t =>
                {
                    DateTime requestOver = DateTime.UtcNow;
                    int milliseconds = (requestOver - requestStart).Milliseconds;
                    logger.Info($"After request: The request execute {milliseconds} seconds.");
                    return t.Result;
                }, cancellationToken);
        }
    }
}