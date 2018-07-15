using System.Collections.Generic;
using System.Threading.Tasks;
using Declarations.DomainModel;

namespace Declarations.Interfaces.Query
{
    public interface IDataRetriever
    {
        Task<IList<GitRepository>> GetRepositoryByCity(string city);
    }
}