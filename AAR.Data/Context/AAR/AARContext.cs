using AAR.Model.Entity.Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace AAR.Data.Context.AAR
{
    public class AARContext : DbContext
    {
        private IConfiguration _configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_configuration["DataSource"]}");
        // there should be no issues in .net core 3.0+
        public AARContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<ExampleEntity> Examples { get; set; }
    }
}