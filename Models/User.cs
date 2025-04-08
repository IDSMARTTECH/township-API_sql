﻿
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.CompilerServices;
using Township_API.Models;


namespace Township_API.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    //User
    [Table("tblUser")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UID { get; set; }

        [Required, MaxLength(250)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string? Phone { get; set; }
        public bool? isactive { get; set; } = false;
        public bool? isdeleted { get; set; } = false;
        public int? createdby { get; set; }
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; }
        public DateTime? updatedon { get; set; } = null;

        [ForeignKey("Role")]
        public int? RoleID { get; set; }
        public Role? Role { get; set; }
    }

    // Profile
    [Table("[tblProfile]")]
    public class Profile
    {
        // [ID],[profilename]     ,[uid]      ,[isactive]      ,[isdeleted]      ,[createdby]      ,[createdon]      ,[updatedby]      ,[updatedon]
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string? profilename { get; set; }

        [ForeignKey("User")]
        public int uid { get; set; }

        public User? user { get; set; }
        public bool? isactive { get; set; } = false;
        public bool? isdeleted { get; set; } = false;
        public int? createdby { get; set; }
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; }
        public DateTime? updatedon { get; set; } = null;

    }

    //ProfileDetails tblProfileDetails
    [Table("[tblProfileDetails]")]
    public class ProfileDetails
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Profile")]
        public int? profileid { get; set; }
        public Profile? profile { get; set; }

        [ForeignKey("Module")]
        public int? moduleId { get; set; }
        public Module? module { get; set; }


        public bool? CanInsert { get; set; } = false;
        public bool? CanUpdate { get; set; } = false;
        public bool? CanDelete { get; set; } = false;
        public bool? CanView { get; set; } = true;
        public bool? isactive { get; set; } = false;
        public bool? isdeleted { get; set; } = false;
        public int? createdby { get; set; }
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; }
        public DateTime? updatedon { get; set; } = null;
    }

    //public class RolePermission
    //{
    //    [Key]
    //    public int PermissionID { get; set; }

    //    [ForeignKey("Role")]
    //    public int RoleID { get; set; } 
    //    public Roles Role { get; set; }

    //    [ForeignKey("Module")]
    //    public int ModuleID { get; set; } 
    //    public Module Module { get; set; }

    //    public bool CanInsert { get; set; } = false;
    //    public bool CanUpdate { get; set; } = false;
    //    public bool CanDelete { get; set; } = false;
    //    public bool CanView { get; set; } = true;

    //    public bool? isactive { get; set; } = false;
    //    public bool? isdeleted { get; set; } = false;
    //    public int? createdby { get; set; }
    //    public DateTime? createdon { get; set; } = null;
    //    public int? updatedby { get; set; }
    //    public DateTime? updatedon { get; set; } = null;
    //}
    [Table("[tblModules]")]
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }

        [Required, MaxLength(100)]
        public string? ModuleName { get; set; }
    }

    [Table("[tblRole]")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(100)]
        public string? RoleName { get; set; }
    }


    [Table("tblModuleData")]
    public class ModuleData
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Code { get; set; }
        [Required, MaxLength(50)]
        public int? TypeID { get; set; } = null;
        public int ParentID { get; set; } = 0;
        public string? ModuleType { get; set; } = null;
        public string? Discriminator { get; set; } = null;
         
        public bool? isactive { get; set; } = false;
        public int? createdby { get; set; } = null;
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; } = null;
        public DateTime? updatedon { get; set; } = null;
         
    }

    public abstract class ModuleDataTemplate : ModuleData 
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Code { get; set; }
        [Required, MaxLength(50)]        
        public int ParentID { get; set; } = 0;
         
        public int status { get; set; }
            public int isDeleted { get; set; }
   
      //  public int? TypeID { get; set; } = null;
        
    }

    public class NRD : ModuleData
    {
       
    }
    public class Building : ModuleData
    {
         
    }
    public class ReaderType : ModuleData { }
    public class Amenities : ModuleData { }
    public class Phase : ModuleData { }
    public class ReaderLocation : ModuleData { }
    public class VehicleMake : ModuleData { }
    public class ReaderRelay : ModuleData { }
    public class ServiceType : ModuleData { }
    public class ContractorType : ModuleData { }
    public class VehicleType : ModuleData { }
    public class ReaderMode : ModuleData { }
    public class ReaderLocations : ModuleData { }


    [Table("tblServiceProvider")]
    public class Service_Provider
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string ServiceProviderID { get; set; }
        public string role { get; set; }
        public bool? isactive { get; set; } = false;
        public int? createdby { get; set; } = null;
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; } = null;
        public DateTime? updatedon { get; set; } = null;
    }

    [Table("tblVehicle")]
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string RegNo { get; set; }
        public string vType { get; set; }
        public string vMake { get; set; }
        public string vColor { get; set; }
        public string TagUID { get; set; }
        public DateTime? PrintedTagID { get; set; }
        public DateTime? TagEncodingDate { get; set; }
        public string Logical_Delete { get; set; }
        public string StickerNo { get; set; }
        public bool? isactive { get; set; } = false;
        public int? createdby { get; set; } = null;
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; } = null;
        public DateTime? updatedon { get; set; } = null;
    }

    [Table("tblUserNRDAccess")]
    public class UserNRDAccess
    {
        public int ID { get; set; }
        public int NRD { get; set; }
        public int userID { get; set; }
        public int sun { get; set; }
        public int mon { get; set; }
        public int tus { get; set; }
        public int wed { get; set; }
        public int thu { get; set; }
        public int fri { get; set; }
        public int sat { get; set; }
        public DateTime? validTillDate { get; set; }
        public bool? isactive { get; set; } = false;
        public int? createdby { get; set; } = null;
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; } = null;
        public DateTime? updatedon { get; set; } = null;
    }

    [Table("tblUserBuildingAccess")]
    public class UserBuildingAccess
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public int sun { get; set; }
        public int mon { get; set; }
        public int tus { get; set; }
        public int wed { get; set; }
        public int thu { get; set; }
        public int fri { get; set; }
        public int sat { get; set; }
        public DateTime? validTillDate { get; set; }
        public int userID { get; set; }
        public int isAcive { get; set; }
        public int? createdby { get; set; } = null;
        public DateTime? createdon { get; set; } = null;
        public int? updatedby { get; set; } = null;
        public DateTime? updatedon { get; set; } = null;
    }



}