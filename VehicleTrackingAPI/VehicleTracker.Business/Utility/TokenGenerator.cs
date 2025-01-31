﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VehicleTracker.Contracts.Models.AppSettingsModels;
using VehicleTracker.Contracts.Models.HelperModels;

namespace VehicleTracker.Business.Utility
{
    public class TokenGenerator
    {
        public IJwtConfig JwtConfig{ get; set; }

        private readonly IJwtConfig _config;
        private readonly JwtSecurityToken _token;
        
        public TokenGenerator(IJwtConfig config, User user)
        {
            _config = config;
            _token = BuildToken(user);
        }

        public string GetToken()
        {
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }

        private JwtSecurityToken BuildToken(User user)
        {
            var claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            issuer: _config.Issuer, 
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config.ExpireTimeInMins)),
                            signingCredentials: creds);
            return token;
        }

        private static IEnumerable<Claim> GetClaims(User user)
        {
            var roleClaimType = user.Role.Equals(Constants.AdminUserRole, StringComparison.CurrentCultureIgnoreCase)
                                ? Constants.AdminUserPolicy
                                : Constants.OtherUserPolicy;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(roleClaimType, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return claims;
        }
    }
}
