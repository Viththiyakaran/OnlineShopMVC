using Microsoft.EntityFrameworkCore;
using OnlineStoreSara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreSara.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Manufacturer> manufacturers { get; set; }

       public DbSet<Product> products { get; set; }

        public DbSet<Users> users { get; set; }

        public DbSet<BillHeader> billHeader { get; set; }

        public DbSet<BillDetail> billDetail { get; set; }
    }
}
