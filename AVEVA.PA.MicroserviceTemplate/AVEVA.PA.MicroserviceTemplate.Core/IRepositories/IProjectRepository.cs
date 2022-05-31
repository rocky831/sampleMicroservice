

using AVEVA.PA.DataAccess.Models;

namespace AVEVA.PA.MicroserviceTemplate.Core.IRepositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByAssetId(int assetId);
    }
}
