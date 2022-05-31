using AVEVA.PA.DataAccess;
using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(PaDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Project>> GetProjectsByAssetId(int assetId)
        {
            return await Context.Projects.Where(p => p.AssetId == assetId).ToListAsync();
        }
    }
}
