using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Distro.Seedworks.Infrastructure.DataAccess
{
    public abstract class DbTestsBase<T> : IDisposable
        where T : DbContext
    {
        protected T _context;
        private TransactionScope _transaction;

        public DbTestsBase(string connectionStringName = "DefaultConnection")
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();



            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            _context = (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);

            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.MaxValue);
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
