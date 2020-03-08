using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblPatient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMan { get; set; }
        public string BirthDate { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string PassNoOrIdentification { get; set; }
        public string HelthCode { get; set; }
        public string Email { get; set; }
        public string TellNo { get; set; }
        public string Address { get; set; }
        public long Payed { get; set; }
        public long CoShare { get; set; }
        public string DateReleased { get; set; }
        public int UserPassId { get; set; }
        public int Status { get; set; }
        public string TimeRegistered { get; set; }
        public string ParentalName { get; set; }
        public string Job { get; set; }
        public string Insurance { get; set; }
        public string HelpName { get; set; }
        public string HelpJob { get; set; }
        public string HelpPassNo { get; set; }
        public string HelpTelNo { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblPatient(TblPatient patient, HttpStatusCode statusEffect)
        {
            Id = patient.Id;
            Name = patient.Name;
            IsMan = patient.IsMan;
            BirthDate = patient.BirthDate;
            CountryId = patient.CountryId;
            CityId = patient.CityId;
            PassNoOrIdentification = patient.PassNoOrIdentification;
            HelthCode = patient.HelthCode;
            Email = patient.Email;
            TellNo = patient.TellNo;
            Address = patient.Address;
            Payed = patient.Payed;
            CoShare = patient.CoShare;
            DateReleased = patient.DateReleased;
            UserPassId = patient.UserPassId;
            Status = patient.Status;
            TimeRegistered = patient.TimeRegistered;
            ParentalName = patient.ParentalName;
            Job = patient.Job;
            Insurance = patient.Insurance;
            HelpName = patient.HelpName;
            HelpJob = patient.HelpJob;
            HelpPassNo = patient.HelpPassNo;
            HelpTelNo = patient.HelpTelNo;

            StatusEffect = statusEffect;
        }

        public DtoTblPatient()
        {
        }
    }
}