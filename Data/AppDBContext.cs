
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Reflection;
using Township_API.Models;
using Module = Township_API.Models.Module; 
using Microsoft.AspNetCore.Mvc;

namespace Township_API.Data
{

    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<User> UserRegisters { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Module> Modules { get; set; }
        //  public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }

        public DbSet<ModuleData> ModuleData { get; set; }
               public DbSet<Vehicle> Vehicles { get; set; }

         
        public DbSet<PrimaryLandowner> Landowners { get; set; }
        public DbSet<DependentLandOwner> DependentLandowners { get; set; }
        public DbSet<PrimaryTenent> PrimaryTenents { get; set; }
        public DbSet<DependentTenent> DependentTenents { get; set; }

        public DbSet<PrimaryResident> PrimaryResidents { get; set; }
        public DbSet<DependentResident> DependentResidents { get; set; }


        public DbSet<Contractor>Contractors { get; set; }
        public DbSet<DependentContractor>DependentContractors { get; set; }

        public DbSet<Service_Provider> ServiceProviders { get; set; }
        public DbSet<ModuleData> ModuleDatas { get; set; }

         
        public IQueryable<NRD> NRDs => (IQueryable<NRD>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.NRD && n.ID > (int)commonTypes.ModuleTypes.NRD);
        public IQueryable<Building> Buildings => (IQueryable<Building>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.Building && n.ID > (int)commonTypes.ModuleTypes.Building);
        public IQueryable<VehicleType> VehicleTypes => (IQueryable<VehicleType>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.VehicleType && n.ID > (int)commonTypes.ModuleTypes.VehicleType);
        public IQueryable<ReaderType> ReaderTypes => (IQueryable<ReaderType>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ReaderType && n.ID > (int)commonTypes.ModuleTypes.ReaderType);
        public IQueryable<ReaderMode> ReaderModes => (IQueryable<ReaderMode>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ReaderMode && n.ID > (int)commonTypes.ModuleTypes.ReaderMode);
        public IQueryable<ContractorType> ContractorTypes => (IQueryable<ContractorType>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ContractorType && n.ID > (int)commonTypes.ModuleTypes.ContractorType);
        public IQueryable<Amenities> Amenities_ => (IQueryable<Amenities>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.Amenities && n.ID > (int)commonTypes.ModuleTypes.Amenities);
        public IQueryable<Phase> Phases => (IQueryable<Phase>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.Phases && n.ID > (int)commonTypes.ModuleTypes.Phases);
        public IQueryable<ReaderLocation> ReaderLocations => (IQueryable<ReaderLocation>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ReaderLocations && n.ID > (int)commonTypes.ModuleTypes.ReaderLocations);
        public IQueryable<ServiceType> ServiceTypes => (IQueryable<ServiceType>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ServiceType && n.ID > (int)commonTypes.ModuleTypes.ServiceType);
        public IQueryable<ReaderRelay> ReaderRelays => (IQueryable<ReaderRelay>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.ReaderRelays && n.ID > (int)commonTypes.ModuleTypes.ReaderRelays);
        public IQueryable<VehicleMake> VehicleMakes => (IQueryable<VehicleMake>)ModuleDatas.Where(n => n.TypeID == (int)commonTypes.ModuleTypes.VehicleMake && n.ID > (int)commonTypes.ModuleTypes.VehicleMake);

        public DbSet<DoorAccess> _userDoorAccess { get; set; }

        public DbSet<UserAmenitiesAccess> _userAmenitiesAccess { get; set; }
        public DbSet<UserNRDAccess> _userNRDAccess { get; set; }

        public DbSet<UserBuildingAccess> _userBuildingAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.Entity<UserNRDAccess>(entity =>
            {
                entity.HasNoKey(); // Views often don’t have a primary key
                entity.ToView("vwNRDDoorAccess"); // Name of the view in SQL
                // Optional: configure column mapping if needed
                // entity.Property(e => e.Name).HasColumnName("SomeColumn");
            });
            modelBuilder.Entity<UserBuildingAccess>(entity =>
            {
                entity.HasNoKey(); // Views often don’t have a primary key
                entity.ToView("vwBuildingDoorAccess"); // Name of the view in SQL
                                                        // Optional: configure column mapping if needed
                                                        // entity.Property(e => e.Name).HasColumnName("SomeColumn");
            }); 
            modelBuilder.Entity<UserAmenitiesAccess>(entity =>
            {
                entity.HasNoKey(); // Views often don’t have a primary key
                entity.ToView("vwAmenitiesDoorAccess"); // Name of the view in SQL
                                                        // Optional: configure column mapping if needed
                                                        // entity.Property(e => e.Name).HasColumnName("SomeColumn");
            });


        }
    }
}
