using Authenticate.Domain.Entities;
using Authenticate.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.UseCases.Create
{
    public class CreateResponse : Response
    {
        protected CreateResponse(){ }

        public CreateResponse(string message, int status, List<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
            Notifications = notifications;
        }

        public CreateResponse(string message, CreateResponseData data)
        {
            Message = message;
            Status = 201;
            Notifications = null;
            Data = data;
        }
        public CreateResponseData? Data { get; set; }
    }
    public record CreateResponseData(Guid Id, string Name, string Email);
}
