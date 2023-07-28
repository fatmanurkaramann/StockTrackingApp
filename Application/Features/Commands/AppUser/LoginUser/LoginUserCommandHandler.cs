using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTrackingApp.Application.Abstraction.Token;
using StockTrackingApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(SignInManager<Domain.Entities.Identity.AppUser> signInManager,
            UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail)
         ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            
            if (user == null)
            {
                throw new Exception("Kullanıcı veya şifre hatalı");
            }

           
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded) // Authentication succeeded
            {
               
                Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserCommandResponse { Token = token };
            }

            return new LoginUserCommandResponse { Message = "Hata!" };


        }
    }
}
