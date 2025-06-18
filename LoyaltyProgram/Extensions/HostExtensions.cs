using System;
using Microsoft.Extensions.Hosting;

namespace LoyaltyProgram.Extensions
{
    public static class HostExtensions
    {
        public static T StartService<T>(this IHost host)
        {
            host.Start();
            var service = (T)host.Services.GetService(typeof(T));
            return service;
        }
    }
}
