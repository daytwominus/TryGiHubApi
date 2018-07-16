using System.Collections.Generic;
using System.Threading.Tasks;
using Declarations.DomainModel;

namespace Declarations.Interfaces.Query
{
    public interface IDataRetriever
    {
        Task<IList<GitRepository>> GetRepositoriesByCity(string city);
        Task<IList<GitRepository>> GetRepositoryByUser(string city);
        Task<GitUser> GetUserByName(string user);
    }
}