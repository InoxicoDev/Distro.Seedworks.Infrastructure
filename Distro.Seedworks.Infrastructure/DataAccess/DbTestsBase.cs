using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Distro.Seedworks.Infrastructure.DataAccess
{
    public abstract class DbTestsBase<T> : IDisposable
        where T : DbContext
    {
        protected T _context;

        public DbTestsBase(string connectionStringName = "DefaultConnection")
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionStringName));

            _context = (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);            
        }

        public void Dispose()
        {            
        }
    }
}
