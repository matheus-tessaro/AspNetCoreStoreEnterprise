using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace SE.WebApp.MVC.Extensions
{
    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicyExtensions() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromMilliseconds(600),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2)
                },
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Retrying for the {retryCount} time...");
                    Console.ForegroundColor = ConsoleColor.White;
                });
    }
}
