using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Entities
{
    public class MyDbContext : IdentityDbContext<MyUser>
    {
        public DbSet<Post> Posts { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) 
            : base(options)
        { }
    }
}
