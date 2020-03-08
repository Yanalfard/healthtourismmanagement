using DashBoard.Models.Regular;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for PatientTreeView.xaml
    /// </summary>
    public partial class PatientTreeView : Page
    {
        public TblPatient Patient { get; set; }
        public TblCountry Country { get; set; }
        public TblCity City { get; set; }
        public TblUserPass UserPass { get; set; }
        public TblHospital Hospital { get; set; }
        public MainWindow MainWindow { get; set; }

        public PatientTreeView()
        {
            InitializeComponent();
        }

        public PatientTreeView(
            MainWindow mainWindow, TblPatient patient, 
            TblCountry country, TblCity city, 
            TblUserPass userPass)
        {
            this.Patient = patient;
            this.Country = country;
            this.City = city;
            this.UserPass = userPass;
            this.MainWindow = mainWindow;
            //this.Hospital = hospital;

            InitializeComponent();

            FillFields();

        }

        private void FillFields()
        {
            switch (Patient.Status)
            {
                case (int)PatientStatus.Registered:
                    EdtxtName.Text = Patient.Name ?? "Name";
                    EdtxtParentalName.Text = Patient.ParentalName ?? "ParentalName";
                    EdRadioFemale.IsChecked = !Patient.IsMan;
                    EdRadioMale.IsChecked = Patient.IsMan;
                    EdComboCity.Text = City.Name ?? "CityName";
                    EdComboCountry.Text = Country.Name ?? "CountryName";
                    EdtxtPassNoOrIdentification.Text = Patient.HelpPassNo ?? "HelpPassNo";
                    EdtxtEmail.Text = Patient.Email ?? "Email";
                    EdtxtTellNumber.Text = Patient.TellNo ?? "TellNo";
                    EdtxtAddress.Text = Patient.Address ?? "Address";
                    EdtxtPassword.Text = UserPass.Password ?? "Password";
                    EdtxtJob.Text = Patient.Job ?? "Job";
                    EdtxtInsurance.Text = Patient.Insurance ?? "Insurance";
                    EdtxtHelpJob.Text = Patient.HelpJob ?? "HelpJob";
                    EdtxtHelpName.Text = Patient.HelpName ?? "HelpName";
                    EdtxtHelpTelNo.Text = Patient.HelpTelNo ?? "HelpTelNo";
                    EdtxtHelpPassNo.Text = Patient.HelpPassNo ?? "HelpPassNo";
                    EdDatePickerTimeRegistered.Text = Patient.DateReleased ?? "DateReleased";


                    break;
                case (int)PatientStatus.Entered:
                    break;
                case (int)PatientStatus.Reception:
                    EdtxtHealthCode.Text = Patient.HelthCode ?? "HelthCode";
                    break;
                case (int)PatientStatus.Served:
                    break;
            }
            EdDatePickerBirthDate.Text = Patient.BirthDate ?? "BirthDate";
            EdDatePickerReleaseDate.Text = Patient.DateReleased ?? "DateReleased";
            EdCheckBoxIsHelthApple.IsChecked = UserPass.IsHelthApple;
            EdtxtAmountPayed.Text = Patient.Payed.ToString();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.CollapseFrames();
            MainWindow.FramePatientView.Visibility = Visibility.Visible;
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAccept_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void BtnCancel_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
