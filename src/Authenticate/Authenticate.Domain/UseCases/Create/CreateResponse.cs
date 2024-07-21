using Flunt.Notifications;

namespace Authenticate.Domain.UseCases.Create
{
    public class CreateResponse : Response
    {
        protected CreateResponse(){ }

        // BADREQUEST CONSTRUCTOR
        public CreateResponse(string message, int status, IEnumerable<Notification>? notifications = null)
        {  
            Message = message;
            Status = status;
            Notifications = notifications;
        }

        // OK CONSTRUCTOR
        public CreateResponse(string message, CreateResponseData data) 
        {
            Message = message;
            Status = 201;
            Notifications = null;
            Data = data;
        }
        public CreateResponseData? Data { get; set; }
    }

    // DADOS DA RESPOSTA
    public record CreateResponseData(Guid Id, string Name, string Email); 
}
