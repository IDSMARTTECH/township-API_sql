namespace Township_API.Models
{
   
    public class commonTypes
    {
        public enum ModuleTypes
        {
            NRD = 2,
            Building = 3,
            VehicleType = 4,
            ReaderType = 5,
            ReaderMode = 6,
            ContractorType = 7,
            Amenities = 8,
            Phases = 9,
            ReaderLocations = 10,
            VehicleMake = 11,
            ReaderRelays = 12,
            ServiceType = 13
        };

    }
    public class DependentJsonWrapper
    {
        public object? Owners { get; set; }
        public object? DependentOwners { get; set; }
        public object? Vehicles { get; set; }
        public object? UserNRDAccess { get; set; }
        public object? UserBuildingAccess { get; set; }
        public object? UserAminitiesAccess { get; set; }

    }


}
