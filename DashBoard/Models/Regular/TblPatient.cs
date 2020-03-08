namespace DashBoard.Models.Regular
{
    public class TblPatient
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

        public TblPatient(int id)
        {
            this.Id = id;
        }

		public TblPatient(int id, string name, bool isMan, string birthDate, int countryId, int cityId, string passNoOrIdentification, string helthCode, string email, string tellNo, string address, long payed, long coShare, string dateReleased, int userPassId, int status, string timeRegistered, string parentalName, string job, string insurance, string helpName, string helpJob, string helpPassNo, string helpTelNo)
        {
            this.Id = id;
            Name = name;
            IsMan = isMan;
            BirthDate = birthDate;
            CountryId = countryId;
            CityId = cityId;
            PassNoOrIdentification = passNoOrIdentification;
            HelthCode = helthCode;
            Email = email;
            TellNo = tellNo;
            Address = address;
            Payed = payed;
            CoShare = coShare;
            DateReleased = dateReleased;
            UserPassId = userPassId;
            Status = status;
            TimeRegistered = timeRegistered;
            ParentalName = parentalName;
            Job = job;
            Insurance = insurance;
            HelpName = helpName;
            HelpJob = helpJob;
            HelpPassNo = helpPassNo;
            HelpTelNo = helpTelNo;
        }
        public TblPatient(string name, bool isMan, string birthDate, int countryId, int cityId, string passNoOrIdentification, string helthCode, string email, string tellNo, string address, long payed, long coShare, string dateReleased, int userPassId, int status, string timeRegistered, string parentalName, string job, string insurance, string helpName, string helpJob, string helpPassNo, string helpTelNo)
        {
            Name = name;
            IsMan = isMan;
            BirthDate = birthDate;
            CountryId = countryId;
            CityId = cityId;
            PassNoOrIdentification = passNoOrIdentification;
            HelthCode = helthCode;
            Email = email;
            TellNo = tellNo;
            Address = address;
            Payed = payed;
            CoShare = coShare;
            DateReleased = dateReleased;
            UserPassId = userPassId;
            Status = status;
            TimeRegistered = timeRegistered;
            ParentalName = parentalName;
            Job = job;
            Insurance = insurance;
            HelpName = helpName;
            HelpJob = helpJob;
            HelpPassNo = helpPassNo;
            HelpTelNo = helpTelNo;
        }

        public TblPatient()
        {
            
        }
    }
}