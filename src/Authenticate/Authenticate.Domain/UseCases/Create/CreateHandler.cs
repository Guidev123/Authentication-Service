using Authenticate.Domain.AccountContext.ValueObjects;
using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Create.Contracts;
using Authenticate.Domain.ValueObjects;

namespace Authenticate.Domain.UseCases.Create
{
    public class CreateHandler
    {
        private readonly IRepository _repository;
        private readonly IService _service;


        public CreateHandler(IRepository repository,
                             IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            // ============== VALIDAR REQUISIÇÃO ==============
            try
            {
                var response = CreateSpecification.Assert(request);
                if (!response.IsValid) return new CreateResponse("The request failed", 400, response.Notifications);
            }
            catch
            {

                return new CreateResponse("Something has failed", 500);
            }

            // ============== GERAR ENTIDADES ==============
            Email email;
            Password password;
            User user;

            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {

                return new CreateResponse(ex.Message, 400);
            }

            // ============== VALIDA SE O USUARIO JA EXISTE ==============
            var exists = await _repository.ExistsAsync(request.Email, cancellationToken);
            if (exists) return new CreateResponse("Your email already exists", 400);
        }
    }
}
