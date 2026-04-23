using System;
using Loyalty.Common.Shared.Extensions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Logging.AppInsights
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor accessor;

        private const string City = "User_City";
        private const string Phone = "User_Phone";
        private const string Role = "User_Role";

        public TelemetryInitializer(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var ctx = accessor.HttpContext;

            if (ctx != null)
            {
                var requestTelemetry = telemetry as RequestTelemetry;

                if (requestTelemetry != null)
                {
                    var userId = accessor.HttpContext.User?.GetUserId();

                    if (!String.IsNullOrEmpty(userId))
                    {
                        var city = accessor.HttpContext.User?.GetCity();
                        var phone = accessor.HttpContext.User?.GetPhone();
                        var role = accessor.HttpContext.User?.GetRole().ToString();

                        if (accessor.HttpContext.User != null && !String.IsNullOrEmpty(userId))
                        {
                            telemetry.Context.User.Id = userId;
                            telemetry.Context.User.AuthenticatedUserId = userId;

                            requestTelemetry.Properties.Add(City, city);
                            requestTelemetry.Properties.Add(Phone, phone);
                            requestTelemetry.Properties.Add(Role, role);
                        }
                    }
                }
            }
        }
    }
}
