using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AppDbCOntext : IdentityDbContext<IdentityUser>
    {
      
            public AppDbCOntext(DbContextOptions<AppDbCOntext>options) : base(options)
        {

            }
            public DbSet<studentinfo> studentinfo { get; set; }
        }
    }
