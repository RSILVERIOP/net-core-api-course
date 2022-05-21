using System;
using Api.Domain.Dtos;
using Api.Domain.Security;
using Api.Domain.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Principal;
using Api.Domain.Interfaces.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IConfiguration _configuration {get;}

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration)
        {
            _repository = repository;    
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new Domain.Entites.UserEntity();

            if (user==null || string.IsNullOrWhiteSpace(user.Email))
            {
                return ReturnAuthenticatedFailed();
            }

            baseUser = await _repository.FindByLogin(user.Email);
            if (baseUser == null)
            {
                return ReturnAuthenticatedFailed();
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(baseUser.Email),
                new []
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),                    
                }
            );
            DateTime createDate = DateTime.Now;
            DateTime expirateDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
            
            string token = CreateToken(identity, createDate, expirateDate);

            return SuccessObject(createDate, expirateDate, token, user);
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirateDate)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigninCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirateDate,
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "User logged in successfully"
            };
        }
        private object ReturnAuthenticatedFailed()
        {
            return new {
                    authenticated = false, 
                    message = "Failed to authenticate"
                };
        }
    }
}