using System;
using AVEVA.PA.DataAccess;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Microservice.UnitTests.Infrastructure
{
    public class BaseDbContextTest : IDisposable
    {
        protected readonly PaDbContext DbContext;
        public BaseDbContextTest()
        {
            var options = new DbContextOptionsBuilder<PaDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            this.DbContext = new PaDbContext(options);
            this.DbContext.Database.EnsureCreated();

            DbInitializerTest.Initialize(this.DbContext);
        }
        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.DbContext.Dispose();
        }
    }
}
