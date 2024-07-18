using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.ValueObjects
{
    public class EmailVerification
    {
        public EmailVerification() { }  //EF CTOR
        protected string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(10);
        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public void VerifyCode(string code)
        {
            if (IsActive) throw new Exception("This code has been inactive");
            if (ExpiresAt < DateTime.UtcNow) throw new Exception("This code has been expired");
            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase)) throw new Exception("Invalid code");
            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}
