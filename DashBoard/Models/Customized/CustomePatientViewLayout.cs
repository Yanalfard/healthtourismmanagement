using System;

namespace DashBoard.Models.Customized
{
    public class CustomePatientViewLayout
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMan { get; set; }

        public DateTime BirthDate { get; set; }

        public string PassNoOrIdentification { get; set; }

        public string HelthCode { get; set; }

        public string Email { get; set; }

        public string TellNo { get; set; }

        public string Address { get; set; }

        public long Payed { get; set; }

        public long CoShare { get; set; }

        public DateTime DateReleased { get; set; }

        public int Status { get; set; }

        public DateTime TimeRegistered { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public CustomePatientViewLayout(int id)
        {
            this.Id = id;
        }

        public CustomePatientViewLayout(int id, string name, bool isMan, DateTime birthDate, string passNoOrIdentification, string helthCode, string email, string tellNo, string address, long payed,long coShare, DateTime dateReleased, int status, DateTime timeRegistered, string countryName, string cityName, string username, string password)
        {
            this.Id = id;
            Name = name;
            IsMan = isMan;
            BirthDate = birthDate;
            PassNoOrIdentification = passNoOrIdentification;
            HelthCode = helthCode;
            Email = email;
            TellNo = tellNo;
            Address = address;
            Payed = payed;
            CoShare = coShare;
            DateReleased = dateReleased;
            Status = status;
            TimeRegistered = timeRegistered;
            CountryName = countryName;
            CityName = cityName;
            Username = username;
            Password = password;
        }

        public CustomePatientViewLayout(string name, bool isMan, DateTime birthDate, string passNoOrIdentification, string helthCode, string email, string tellNo, string address, long payed, long coShare, DateTime dateReleased, int status, DateTime timeRegistered, string countryName, string cityName, string username, string password)
        {
            Name = name;
            IsMan = isMan;
            BirthDate = birthDate;
            PassNoOrIdentification = passNoOrIdentification;
            HelthCode = helthCode;
            Email = email;
            TellNo = tellNo;
            Address = address;
            Payed = payed;
            CoShare = coShare;
            DateReleased = dateReleased;
            Status = status;
            TimeRegistered = timeRegistered;
            CountryName = countryName;
            CityName = cityName;
            Username = username;
            Password = password;
        }
    }
}