using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.Models;

namespace WebApiRest.Contexts
{
    public class MyApplicationDbContext : IdentityDbContext<MyApplicationUser>
    {
        public MyApplicationDbContext( DbContextOptions<MyApplicationDbContext> options ) 
            :base(options )
        {

        }
    }
}
