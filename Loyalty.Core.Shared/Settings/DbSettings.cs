using System;

namespace Loyalty.Core.Shared.Settings
{
    public class DbSettings
    {
        public string Host { get; set; }

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseSsl { get; set; }

        public int Port { get; set; } = 10255;
    }
}
