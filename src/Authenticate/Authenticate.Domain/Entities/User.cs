using Authenticate.Domain.AccountContext.ValueObjects;
using Authenticate.Domain.ValueObjects;
using HealthManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.Entities
{
    public class User : Entity
    {
        public User(string email, string? password = null)
        {
            Email = email;
            Password = new Password(password);
        }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;

        public void UpdatePassword(string plainedTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Update code is inválid");
            }
            var password = new Password(plainedTextPassword);
            Password = password;
        }
        public void ChangePassword(string plainedTextPassword)
        {
            var password = new Password(plainedTextPassword);
            Password = password;
        }
        public void UpdateEmail(Email email) => Email = email;
    }
}
