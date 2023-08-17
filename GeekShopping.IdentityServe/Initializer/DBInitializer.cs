using GeekShopping.IdentityServe.Configuration;
using GeekShopping.IdentityServe.Domain;
using GeekShopping.IdentityServe.Domain.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServe.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly IdentityMySqlContext _Context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DBInitializer(IdentityMySqlContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _Context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if(_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null)
            {
                return;
            }

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser adminUser = new ApplicationUser()
            {
                FirstName = "Edson",
                UserName = "EdsonAdmin",

                LastName = "Admin",
                Email = "edson-admin@rodrigo.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (73) 99998-4545"
            };

            _user.CreateAsync(adminUser, "tel24M20*").GetAwaiter().GetResult();
            _user.AddToRoleAsync(adminUser, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaim = _user.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName}{adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, $"{adminUser.FirstName}"),
                new Claim(JwtClaimTypes.FamilyName, $"{adminUser.LastName}"),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),

            }).Result;




            ApplicationUser clienteUser = new ApplicationUser()
            {
                FirstName = "Edson",
                LastName = "Client",
                UserName = "EdsonClient",

                Email = "edson-Client@rodrigo.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (73) 89998-2545"
            };

            _user.CreateAsync(clienteUser, "tel24M20*").GetAwaiter().GetResult();
            _user.AddToRoleAsync(clienteUser, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clienteClaim = _user.AddClaimsAsync(clienteUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{clienteUser.FirstName}{clienteUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, $"{clienteUser.FirstName}"),
                new Claim(JwtClaimTypes.FamilyName, $"{clienteUser.LastName}"),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),

            }).Result;
        }
    }
}
