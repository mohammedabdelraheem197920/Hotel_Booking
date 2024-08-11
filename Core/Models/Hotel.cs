using Core.RepositoryInterfaces;

namespace Core.Models
{
    public class Hotel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        List<Branch> Branches { get; set; } = new List<Branch>();
    }
}
