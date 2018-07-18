using AutoMapper;
using Declarations.DomainModel;
using GiHubGrapthQlDataRetriever.Docs;

namespace GiHubGrapthQlDataRetriever
{
    public class AutoMapperRepoProfile : Profile
    {
        public AutoMapperRepoProfile()
        {
            CreateMap<UserDoc, GitUser>(); 
        }
    }

}
