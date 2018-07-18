namespace Declarations.DomainModel
{
    public class GitRepository : IDomainEntity
    {
		public string Name { set; get; }
        public string Description { set; get; }
        public dynamic Stargazers { set; get; }
    }
}
