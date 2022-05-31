using AVEVA.PA.DataAccess;
using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.Repositories
{
    public class TemplateRepository : GenericRepository<AssetTemplate>, ITemplateRepository
    {
        public TemplateRepository(PaDbContext context) : base(context)
        {
           
        }
        public async Task<IEnumerable<AssetTemplate>> GetTemplatesByAssetId(int assetId)
        {
            return await Context.AssetTemplates.Where(t => t.AssetId == assetId).ToListAsync();
        }
    }
}
