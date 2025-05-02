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
        public enum AccessCardHilders
        { 
            Resident = 1,
            DependentResident = 2,    
            Landowner=3,
            DependentLandowner=4,
            Tenent=5,
            DependentTenent=6,
            ServiceProvider=7,
            contractor=8,
            Guest = 9

        };
    }
    public class profileRegister
    {
        public string USER { get; set; }
        public string ModuleName { get; set; }
        public string RoleName { get; set; }
        public string CanInsert { get; set; }
        public string CanUpdate { get; set; }
        public string CanDelete { get; set; }
        public string CanView { get; set; }
    }
    public class DependentJsonWrapper
    {
        public object? Owners { get; set; }
        public object? DependentOwners { get; set; }
        public object? Vehicles { get; set; }
        public object? UserAllAccess { get; set; }
        public object? UserNRDAccess { get; set; }
        public object? UserBuildingAccess { get; set; }
        public object? UserAminitiesAccess { get; set; } 
    }


}
