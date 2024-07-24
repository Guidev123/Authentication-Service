using Authenticate.Domain.AccountContext.ValueObjects;
using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Create.Contracts;
using Authenticate.Domain.ValueObjects;
using MediatR;

namespace Authenticate.Domain.UseCases.Create
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly IRepository _repository;
        private readonly IService _service;


        public CreateHandler(IRepository repository,
                             IService service)
        {
            _repository = repository;
            _service = service;
        }

        // MEDIATR METHOD
        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
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


            // ============== GERAR ENTIDADE ==============
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
            try
            {
                var exists = await _repository.ExistsAsync(request.Email, cancellationToken);
                if (exists) return new CreateResponse("Your email already exists", 400);
            }
            catch 
            {
                return new CreateResponse("Something has failed", 500);
            }


            // ============== PERSISTIR DADOS ==============
            try
            {
                await _repository.SaveAsync(user, cancellationToken);
            }
            catch
            {
                return new CreateResponse("Something has failed", 500);
            }


            // ============== ENVIAR EMAIL PARA ATIVAR CONTA ==============
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            }
            catch
            {
                // (TO DO)
            }

            return new CreateResponse("Account has created", new CreateResponseData(user.Id, user.Name, user.Email));
        }
    }
}
