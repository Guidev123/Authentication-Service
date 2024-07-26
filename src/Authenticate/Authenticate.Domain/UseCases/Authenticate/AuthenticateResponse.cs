using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.UseCases.Authenticate
{
    public class AuthenticateResponse : Response
    {
        protected AuthenticateResponse() { }

        // BADREQUEST CONSTRUCTOR
        public AuthenticateResponse(string message, int status, IEnumerable<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
            Notifications = notifications;
        }

        // OK CONSTRUCTOR
        public AuthenticateResponse(string message, AuthenticateResponseData data)
        {
            Message = message;
            Status = 201;
            Notifications = null;
            Data = data;
        }
        public AuthenticateResponseData? Data { get; set; }
    }
}
