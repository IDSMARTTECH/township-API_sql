using Township_API.Models;

namespace Township_API.Controllers
{
    internal class JsonWrapper
    {
        public List<PrimaryLandowner> owners { get; set; }
        public Task<List<DependentLandOwner>> DependentOwners { get; set; }
        public Task<List<DependentLandOwner>> Vehicles { get; set; }
    }
}