using Microsoft.EntityFrameworkCore;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS_2._0.MVVM.Model;

namespace MVVM
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();//УБРАТЬ!!!
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=test2.0db;Trusted_Connection=True;");
        }
    }
}
