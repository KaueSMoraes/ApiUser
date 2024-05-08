using AssemblyMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyMaster.Data
{
    //Herda do Objeto DbContext
    public class MkmDataContext : DbContext 
    {
        //Cada Propriedade equivale a uma tabela no Banco
        public DbSet<User> USERS { get; set; }
        public DbSet<Role> ROLES { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"));
        }
    }
}