using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Calculator.Microservices.Shared.Library.HealthCheck.SelfHealthCheck
{
    public static class SelfHealthCheckExtension
    {
        private const string NAME = "self";
        public static IHealthChecksBuilder AddSelfCheck(this IHealthChecksBuilder builder, HealthStatus? failureStatus = default,
                                                        IEnumerable<string>? tags = default, TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                NAME,
                new SelfHealthCheck(),
                failureStatus,
                tags,
                timeout
                ));
        }
    }
}
