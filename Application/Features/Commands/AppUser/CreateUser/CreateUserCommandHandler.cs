using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace StockTrackingApp.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.Identity.AppUser>(request);
           IdentityResult result =  await _userManager.CreateAsync(user,request.Password);

            if (result.Succeeded)
            {
                return new()
                {
                    Succeded = true,
                    Message = "Kullanıcı oluştruldu"
                };
            }
            else
            {
                throw new Exception("Hata");

            }
            return new();
        }
    }
}
