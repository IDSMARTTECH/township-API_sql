using System.ComponentModel.DataAnnotations.Schema;

namespace Township_API.Models
{

    [Table("PrimaryResident")]
    public class PrimaryResident
    {
        public int ID { get; set; }
        public string CSN { get; set; }
        public string IDNumber { get; set; }
        public string TagNumber { get; set; }
        public string PANnumber { get; set; }
        public string PassportNo { get; set; }
        public string LicenseNo { get; set; }
        public string ICEno { get; set; }
        public string AadharCardId { get; set; }
        public string VoterID { get; set; }
        public string FirstName { get; set; }
        public string MiddletName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DOB { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string LandLine { get; set; }
        public string? NRD { get; set; }
        public string Building { get; set; }
        public string FlatNumber { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardPrintingDate { get; set; }
        public DateTime? RegistrationIssueDate { get; set; }
        public int LogicalDeleted { get; set; }
    }

    [Table("DependentResident")]
    public class DependentResident
    {
        public int ID { get; set; }
        public int PID { get; set; }        //Primary-ResidentID
        public string CSN { get; set; }
        public string IDNumber { get; set; }
        public string TagNumber { get; set; }
        public string PANnumber { get; set; }
        public string PassportNo { get; set; }
        public string LicenseNo { get; set; }
        public string ICEno { get; set; }
        public string AadharCardId { get; set; }
        public string VoterID { get; set; }
        public string FirstName { get; set; }
        public string MiddletName { get; set; }
        public string LastName { get; set; }
        public string ShortName { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DOB { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string LandLine { get; set; }
        public string Building { get; set; }
        public string FlatNumber { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardPrintingDate { get; set; }
        public DateTime? RegistrationIssueDate { get; set; }
        public int LogicalDeleted { get; set; }
    }


   
}
