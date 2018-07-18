using System.Collections.Generic;

namespace Declarations.DomainModel
{
    public class GitUser
    {
        public string Name { set; get; }
        public string Location { set; get; }
        public List<GitUser> Followers { set; get; } = new List<GitUser>();
    }
}
