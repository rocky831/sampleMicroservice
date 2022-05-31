using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;

namespace AVEVA.PA.MicroserviceTemplate.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///  Saves all changes made in this context to the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task SaveAsync();

        /// <summary>
        /// This method will create a database Transaction
        /// </summary>
        /// <returns></returns> 
        Task CreateTransactionAsync();

        /// <summary>
        /// Save the changes permanently in the database
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// Rollback the database changes to its previous state
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();

        IProjectRepository Projects { get; }
        ITemplateRepository Templates { get; }
    }
}
