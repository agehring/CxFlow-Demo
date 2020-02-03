using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Entities
{
    public class Seeder
    {
        readonly MyDbContext _dbContext;
        readonly UserManager<MyUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(MyDbContext dbContext, UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed() {
            await SeedDatabase();
            await CreateRole(Roles.Admin);
            await SeedUsers();
        }

        async Task CreateRole(string name)
        {
            var role = new IdentityRole
            {
                Name = name
            };

            var result = await _roleManager.CreateAsync(role);
            if(result.Succeeded == false)
            {
                var exception = new InvalidOperationException("Failed to create role " + name);
                exception.Data["Errors"] = result.Errors;
                throw exception;
            }
        }


       async Task SeedUsers()
        {
            await CreateUserWithPasswordAndRoles("attacker", "password");
            await CreateUserWithPasswordAndRoles("admin", "admin", Roles.Admin);
        }

        async Task CreateUserWithPasswordAndRoles(string userName, string password, params string[] roles)
        {
            
            var user = new MyUser
            {
                UserName = userName,
                DisplayName = userName
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded == false)
            {
                var exception = new InvalidOperationException("Failed to create user " + userName);
                exception.Data["Errors"] = createResult.Errors;
                throw exception;
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
            if (addPasswordResult.Succeeded == false)
            {
                var exception = new InvalidOperationException("Failed to set password for user " + userName);
                exception.Data["Errors"] = addPasswordResult.Errors;
                throw exception;
            }

            if(roles.Any()) {
                var addRoleResult = await _userManager.AddToRolesAsync(user, roles);
                if(addRoleResult.Succeeded == false)
                {
                    var exception = new InvalidOperationException("Failed to set roles for user " + userName);
                    exception.Data["Errors"] = addRoleResult.Errors;
                    throw exception;
                }
            }
        }

        async Task SeedDatabase()
        {
            await _dbContext.Posts.AddRangeAsync(
                new Post { Html = "Hello world", Created = DateTimeOffset.Parse("2019-01-01 15:32:01+13") },
                new Post { Html = "This is some seed data", Created = DateTimeOffset.Parse("2019-01-01 15:33:04+13") },
                new Post { Html = "This should be recreated every time the application starts", Created = DateTimeOffset.Parse("2019-01-01 15:35:22+13") }
            );

            await _dbContext.SaveChangesAsync();
        }
    }
}
