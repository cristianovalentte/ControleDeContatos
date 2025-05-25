using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ControleDeContatos.Data
{
    public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
    {
        public BancoContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // pega o appsettings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DataBase"));

            return new BancoContext(optionsBuilder.Options);
        }
    }
}