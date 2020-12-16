using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedbackAPI.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
