using AVEVA.PA.DataAccess;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PaDbContext context;
        private IDbContextTransaction transaction;

        public IProjectRepository Projects { get; private set; }
        public ITemplateRepository Templates { get; private set; }

        public UnitOfWork(PaDbContext context,
                          IProjectRepository ProjectRepository,
                          ITemplateRepository TemplateRepository)
        {
            this.context = context;
            this.Projects = ProjectRepository;
            this.Templates = TemplateRepository;
        }

        /// <summary>
        ///  Saves all changes made in this context to the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// This method will create a database Trnasaction
        /// </summary>
        /// <returns></returns>        
        public async Task CreateTransactionAsync()
        {
            if (transaction == null)
                transaction = await this.context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Save the changes permanently in the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            if (this.transaction != null)
                await this.transaction.CommitAsync();
        }

        /// <summary>
        /// Rollback the database changes to its previous state
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync()
        {
            if (this.transaction != null)
            {
                await this.transaction.RollbackAsync();
                await this.transaction.DisposeAsync();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
