using ClaimsTraining.Data;
using ClaimsTraining.Helpers;
using ClaimsTraining.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ClaimsTraining.Services
{
    public interface ICustomerService
    {
        Customer Authenticate(String CustomerName, String CustomerPass);
        IEnumerable<Customer> GetAll();
    }

    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _Customers = new List<Customer>
        {
            new Customer { CustomerId = 1, FirstName = "Test", LastName = "User", CustomerName = "admin", CustomerPass = "test", Role = new Role{ RoleId=1, RoleName="Admin" } },
            new Customer { CustomerId = 2, FirstName = "Test", LastName = "User", CustomerName = "user", CustomerPass = "test", Role = new Role{ RoleId=1, RoleName="User" } },
        };

        private readonly AppSettings _AppSettings;
        private readonly IConfiguration _IConfig;

        public CustomerService(IConfiguration _IConfig, IOptions<AppSettings> _AppSettings)
        {
            this._IConfig = _IConfig;
            this._AppSettings = _AppSettings.Value;
        }

        public Customer Authenticate(String CustomerName, String CustomerPass)
        {
            DefaultContext DContext = new DefaultContext(_IConfig);
            var _Customer = _Customers.SingleOrDefault(x => x.CustomerName == CustomerName && x.CustomerPass == CustomerPass);

            // return null if user not found
            if (_Customer == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _Customer.CustomerName),
                    new Claim("CompletedBasicTraining", "")

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _Customer.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            _Customer.CustomerPass = null;

            return _Customer;
        }

        public IEnumerable<Customer> GetAll()
        {
            // return users without passwords
            return _Customers.Select(x => {
                x.CustomerPass = null;
                return x;
            });
        }
    }
}
