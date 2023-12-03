using AutoMapper;
using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Auth;
using Blogging_Platform.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogging_Platform.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _Mapper;
        private readonly JwtOption _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IMapper Mapper, Microsoft.Extensions.Options.IOptions<JwtOption> jwt)
        {
            _userManager = userManager;
            _Mapper = Mapper;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> LoginAsync(AuthLoginModle model)
        {
            try
            {
                AuthModel FailProcess()
                {
                    var mappedModel = new AuthModel();
                    mappedModel.Message = "UserName or Password is Icorrect!";
                    return mappedModel;
                }


                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null) {
                    user = await _userManager.FindByNameAsync(model.UserName);
                    if (user == null) return FailProcess();
                } 
               
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result) return FailProcess();
                

                var jwtToken = await CraeteToken(user);
                var roles = await _userManager.GetRolesAsync(user);
                var userModel = _Mapper.Map<AuthModel>(user);
                userModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userModel.ExpiresOn = jwtToken.ValidTo;
                userModel.IsAuthenticated = true;
                userModel.Roles = roles.ToList();

                return userModel;

            }
            catch { throw new Exception(); }
        }

        public async Task<AuthModel> RegisterAsync(AuthRequestModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var mappedModel = _Mapper.Map<AuthModel>(model);
                    mappedModel.Message = "Email Already Registerd!";
                    return mappedModel;
                }
                user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var mappedModel = _Mapper.Map<AuthModel>(model);
                    mappedModel.Message = "UserName Already Registerd!";
                    return mappedModel;
                }
                var authUser = _Mapper.Map<ApplicationUser>(model);
                var result = await _userManager.CreateAsync(authUser, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                    {

                        errors += error.Description + " ,";
                    }
                    var mappedModel = _Mapper.Map<AuthModel>(model);
                    mappedModel.Message = errors;
                    return mappedModel;
                }
                await _userManager.AddToRoleAsync(authUser, "User");

                var jwtToken = await CraeteToken(authUser);
                var userModel = _Mapper.Map<AuthModel>(authUser);
                userModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userModel.ExpiresOn = jwtToken.ValidTo;
                userModel.IsAuthenticated = true;
                userModel.Roles = new List<string> { "User" };

                return userModel;

            }
            catch { throw new Exception(); }
        }

        private async Task<JwtSecurityToken> CraeteToken(ApplicationUser user)
        {

            var userClaim = await _userManager.GetClaimsAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)

            }.Union(userClaim).Union(roleClaims);

            var SymmetricSequrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(SymmetricSequrityKey, SecurityAlgorithms.HmacSha256);
            var jwtSequrityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
                );
            return jwtSequrityToken;


        }
    }
}
