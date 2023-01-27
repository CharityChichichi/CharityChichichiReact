using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityChichichiAPI.Models
{
    public class CustomerDBContext : DbContext
    {
        public DbSet<CustomerModel> CustomerModel { get; set;}

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
            : base(options)
        {

        }
    }
}
