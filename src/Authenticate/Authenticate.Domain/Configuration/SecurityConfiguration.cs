using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.Configuration
{

    public static class SecurityConfiguration
    {
        public static SecurityKeys Secrets { get; set; } = new();
        public static EmailConfig Email { get; set; } = new();

        public class EmailConfig()
        {
            public string DefaultFromEmail { get; set; } = "verify@healtmanager.com";
            public string DefaultFromName { get; set; } = "healthmanager";

        }


        public class SecurityKeys
        {
            public static string ApiKey { get; set; } = string.Empty;
            public static string JwtPrivateKey { get; set; } = string.Empty;
            public static string PasswordSaltKey { get; set; } = string.Empty;
            public static string ConnectionString { get; set;} = string.Empty;
        }
    }
}
