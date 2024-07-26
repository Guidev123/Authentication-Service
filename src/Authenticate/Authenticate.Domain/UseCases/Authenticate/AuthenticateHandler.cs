using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Authenticate.Contracts;
using MediatR;

namespace Authenticate.Domain.UseCases.Authenticate
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly IRepository _repository;

        public AuthenticateHandler(IRepository repository) => _repository = repository;


        // MEDIATR METHOD
        public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            // ============== VALIDAR REQUISIÇÃO ==============
            try
            {
                var response = AuthenticateSpecification.Assert(request);
                if (!response.IsValid) return new AuthenticateResponse("The request failed", 400, response.Notifications);
            }
            catch
            {

                return new AuthenticateResponse("Something has failed", 500);
            }

            // ============== RECUPERAR USUARIO ==============
            User? user;
            try
            {
                user = await _repository.GetUserByEmail(request.Email, cancellationToken);
                if (user == null) return new AuthenticateResponse("User not found", 404);
            }
            catch
            {
                return new AuthenticateResponse("Something has failed", 500);
            }

            // ============== VALIDA SENHA DO USUARIO ==============
            if(!user.Password.VerifyPassword(request.Password)) return new AuthenticateResponse("Invalid credentials", 400);


            // ============== VALIDA VERIFICACAO DA CONTA ==============
            try
            {
                if (!user.Email.EmailVerification.IsActive) return new AuthenticateResponse("Your account is inactive", 400);
            }
            catch
            {
                return new AuthenticateResponse("Something has failed", 500);
            }


            // ============== RETORNAR DADOS ==============
            try
            {
                var data = new AuthenticateResponseData
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = Array.Empty<string>()
                };

                return new AuthenticateResponse(string.Empty, data);
            }
            catch 
            {
                return new AuthenticateResponse("Something has failed", 500);
            }
        }
    }
}
