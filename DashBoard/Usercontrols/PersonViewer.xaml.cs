using DashBoard.Models.Regular;
using System.Windows.Controls;
using System.Windows.Media;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for PersonViewer.xaml
    /// </summary>
    public partial class PersonViewer : UserControl
    {
        public TblPatient Patient { get; set; }
        public TblCountry Country { get; set; }
        public TblCity City { get; set; }
        public TblUserPass UserPass { get; set; }
        public  TblHospital Hospital { get; set; }

        public PersonViewer()
        {
            this.Patient = new TblPatient(-1, "Test", true, "1999/01/01", 1, 1, "PassNo", "HealthCode",
                "Email", "0914", "Address", 1800, 50, "1999/05/01", 1, 1, "1999/04/01", "Testington", "Job", "Insurance", "HelpName",
                "HelpJob", "HelpPassNo", "HelpTelNo");
            this.Country = new TblCountry(
                1,"Country");
            this.City = new TblCity(0,"City",1);
            this.UserPass = new TblUserPass(1,"UserName","Password",true);
            InitializeComponent();
        }

        public PersonViewer(TblPatient patient, TblCountry country, TblCity city, TblUserPass userPass)
        {
            this.Patient = patient;
            this.Country = country;
            this.City = city;
            this.UserPass = userPass;
            InitializeComponent();

            LblCountryCity.Text = Country.Name + "-" + City.Name;
            LblIdOrPass.Text = patient.PassNoOrIdentification;
            LblName.Text = patient.Name + " " + patient.ParentalName;
            LblHospital.Text = string.Empty;
            //imgAvatar
            //brdTag
            switch (Patient.Status)
            {
                case (int)PatientStatus.Registered:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientRegistered");
                    break;
                case (int)PatientStatus.Entered:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientEntered");
                    break;
                case (int)PatientStatus.Reception:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientReception");
                    break;
                case (int)PatientStatus.Served:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientServed");
                    break;
                default:
                    break;
            }
        }

        public PersonViewer(TblPatient patient, TblCountry country, TblCity city, TblUserPass userPass, TblHospital hospital)
        {
            this.Patient = patient;
            this.Country = country;
            this.City = city;
            this.UserPass = userPass;
            this.Hospital = hospital;
            InitializeComponent();

            LblCountryCity.Text = Country.Name + "-" + City.Name;
            LblIdOrPass.Text = patient.PassNoOrIdentification;
            LblName.Text = patient.Name + " " + patient.ParentalName;
            LblHospital.Text = hospital.Name;
            //imgAvatar
            //brdTag
            switch (Patient.Status)
            {
                case (int)PatientStatus.Registered:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientRegistered");
                    break;
                case (int)PatientStatus.Entered:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientEntered");
                    break;
                case (int)PatientStatus.Reception:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientReception");
                    break;
                case (int)PatientStatus.Served:
                    BrdTag.Background = (SolidColorBrush)FindResource("ColorPatientServed");
                    break;
                default:
                    break;
            }
        }

    }
}
