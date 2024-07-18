using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthManager.Domain.Extensions
{
    public static class StringExtension
    {
        public static string ToBase64(this string value) => Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
    }
}
