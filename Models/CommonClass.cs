using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

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
        public enum AccessCardHolders
        { 
            Resident = 1,
            DependentResident = 2,    
            Landowner=3,
            DependentLandowner=4,
            Tenent=5,
            DependentTenent=6,
            ServiceProvider=7,
            contractor=8,
            Guest = 9, 
            Employee = 11,
            DependentContractor = 12,
            Visitor = 13
        };
    }
    public class profileRegister
    {       
        public string profileID { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string profileName { get; set; } 
        public string? CanInsert { get; set; } = "false";
        public string? CanUpdate { get; set; } = "false";
        public string? CanDelete { get; set; } = "false";
        public string? CanView { get; set; } = "false";
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
    
   [Table("images")] 
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; } // BLOB
    }


    [Table("ServiceProviderOwners")]
    public class ServiceProviderOwners
    {
        //Id, SProviderID, 
        [Key]
        public int Id { get; set; }
        [Required]
        public string SProviderID { get; set; }
        [Required] 
        public string Hid { get; set; }
         
        public string? OwnerDetails { get; set; } = "";
        public bool isActive { get; set; } = true;

        [NotMapped]
        public string? OwnerName { get; set; } = ""; 
        [NotMapped]
        public string? building { get; set; } = ""; 
        [NotMapped]
        public string? Neighbourhood { get; set; } = "";
        [NotMapped]
        public string? FlatNumber { get; set; } = "";

    }
    // Id, SProviderID , Hid, OwnerDetails,  isActive


    [Table("AccessBlockRevoke_Register")]
    public class AccessBlockRevoke_Register
    {
        [Key]
        public int id { get; set; }
        public int IDnumber { get; set; }
        public string CardCsn { get; set; }
        public string BlockRevokType { get; set; }
        public DateTime? fromdate { get; set; }
        public DateTime? todate { get; set; }
        public DateTime? createdOn { get; set; }
        public int? createdby { get; set; } = 0;
        public DateTime updatedOn { get; set; } 
        public int updatedby { get; set; } = 0;
    }


    [Table("CardLostDamage_Register")]
    public class CardLostDamage_Register
    { 
        [Key]
        public int id { get; set; }
        public int IDnumber { get; set; }
        public string CardCsn { get; set; }
        public string description { get; set; }
        public string LostDamageType { get; set; }
        public DateTime? reporteddate { get; set; }
        public DateTime? blockeddate { get; set; }
        public DateTime? createdOn { get; set; }
        public int? createdby { get; set; } = 0;
        public DateTime updatedOn { get; set; }
        public int updatedby { get; set; } = 0;
    }



}
