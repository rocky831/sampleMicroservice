using AVEVA.PA.DataAccess.Models;

namespace AVEVA.PA.MicroserviceTemplate.Core.IRepositories
{
    public interface ITemplateRepository : IGenericRepository<AssetTemplate>
    {
        Task<IEnumerable<AssetTemplate>> GetTemplatesByAssetId(int assetId);
    }
}
