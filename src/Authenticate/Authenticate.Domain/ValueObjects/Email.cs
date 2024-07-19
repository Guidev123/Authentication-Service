using Authenticate.Domain.ValueObjects;
using HealthManager.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Authenticate.Domain.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string PATTERN = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public Email(string address)
        {
            Address = address.Trim().ToLower();

            if (string.IsNullOrEmpty(address)) throw new Exception("E-mail inválido");
            if (Address.Length < 5) throw new Exception("E-mail inválido");
            if (!EmailRegex().IsMatch(Address)) throw new Exception("E-mail inválido");
        }

        public string Address { get; }
        public string Hash => Address.ToBase64();
        public EmailVerification EmailVerification { get; private set; } = new();
        public void ReseTVerification() => EmailVerification = new EmailVerification();

        public static implicit operator string(Email email) => email.ToString();

        public static implicit operator Email(string address) => new(address);

        public override string ToString() => Address;

        [GeneratedRegex(PATTERN)]
        private static partial Regex EmailRegex();
    }
}
