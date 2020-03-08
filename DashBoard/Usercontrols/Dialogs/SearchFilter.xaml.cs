using System;
using System.Windows;
using DashBoard.Models.Regular;

namespace DashBoard
{
    public partial class SearchFilter
    {

        public PatientView PatientView { get; }

        public TblCity City { get; set; }
            = new TblCity(-1, "○", -1);
        public TblCountry Country { get; set; }
            = new TblCountry(-1, "○");
        public TblHospital Hospital { get; set; }
            = new TblHospital(-1, "○", -1, -1,
            "○", "○", "○");
        public TblPatient Patient { get; set; }
            = new TblPatient(-1, "○", false, "○", -1, -1,
                "○", "○", "○", "○", "○",
            -1, -1, "○", -1, -1, "○",
                "○", "○", "○", "○", "○",
                "○", "○");

        public MessageBoxResult Result { get; set; }

        public SearchFilter(PatientView patientView)
        {
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            PatientView = patientView;
            InitializeComponent();

        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            Result = MessageBoxResult.Cancel;
        }

        private void BtnAccept_OnClick(object sender, RoutedEventArgs e)
        {
            City.Name = string.IsNullOrWhiteSpace(EdTxtCity.Text) ? "○" : EdTxtCity.Text;
            Country.Name = string.IsNullOrWhiteSpace(EdTxtCountry.Text) ? "○" : EdTxtCountry.Text;
            Hospital.Name = string.IsNullOrWhiteSpace(EdtxtHospitalName.Text) ? "○" : EdtxtHospitalName.Text;

            Hospital.Description = string.IsNullOrWhiteSpace(EdtxtDescription.Text) ? "○" : EdtxtDescription.Text;
            Hospital.Latitude = string.IsNullOrWhiteSpace(EdtxtLatitude.Text) ? "○" : EdtxtLatitude.Text;
            Hospital.Longitude = string.IsNullOrWhiteSpace(EdtxtLongtitude.Text) ? "○" : EdtxtLongtitude.Text;

            Hospital.Percentage = int.TryParse(EdtxtPercentage.Text, out var percentage) ? percentage : -1;

            Patient.HelpPassNo = string.IsNullOrWhiteSpace(EdtxtHelpPassNo.Text) ? "○" : EdtxtHelpPassNo.Text;
            Patient.HelpJob = string.IsNullOrWhiteSpace(EdtxtHelpJob.Text) ? "○" : EdtxtHelpJob.Text;
            Patient.HelpName = string.IsNullOrWhiteSpace(EdtxtHelpName.Text) ? "○" : EdtxtHelpName.Text;
            Patient.HelpTelNo = string.IsNullOrWhiteSpace(EdtxtHelpTelNo.Text) ? "○" : EdtxtHelpTelNo.Text;

            Patient.Name = string.IsNullOrWhiteSpace(EdtxtName.Text) ? "○" : EdtxtName.Text;
            //☺
            Patient.IsMan = false;

            Patient.BirthDate = string.IsNullOrWhiteSpace(EdDatePickerBirthDate.Text) ? "○" : EdDatePickerBirthDate.Text;
            Patient.PassNoOrIdentification =
                string.IsNullOrWhiteSpace(EdtxtPassNoOrIdentification.Text) ? "○" : EdtxtPassNoOrIdentification.Text;

            Patient.HelthCode = string.IsNullOrWhiteSpace(EdtxtHealthCode.Text) ? "○" : EdtxtHealthCode.Text;
            Patient.Email = string.IsNullOrWhiteSpace(EdtxtEmail.Text) ? "○" : EdtxtEmail.Text;
            Patient.TellNo = string.IsNullOrWhiteSpace(EdtxtTellNumber.Text) ? "○" : EdtxtTellNumber.Text;
            Patient.Address = string.IsNullOrWhiteSpace(EdtxtAddress.Text) ? "○" : EdtxtAddress.Text;

            Patient.Payed = long.TryParse(EdtxtAmountPayed.Text, out var payed) ? payed : -1;
            Patient.CoShare = long.TryParse(EdtxtCoShare.Text, out var coshare) ? coshare : -1;

            Patient.DateReleased = string.IsNullOrWhiteSpace(EdDatePickerReleaseDate.Text) ? "○" : EdDatePickerReleaseDate.Text;
            Patient.TimeRegistered = string.IsNullOrWhiteSpace(EdDatePickerTimeRegistered.Text) ? "○" :
                EdDatePickerTimeRegistered.Text;
            Patient.ParentalName = string.IsNullOrWhiteSpace(EdtxtParentalName.Text) ? "○" : EdtxtParentalName.Text;
            Patient.Job = string.IsNullOrWhiteSpace(EdtxtJob.Text) ? "○" : EdtxtJob.Text;
            Patient.Insurance = string.IsNullOrWhiteSpace(EdtxtInsurance.Text) ? "○" : EdtxtInsurance.Text;

            Patient.CountryId = ((TblCountry) EdTxtCountry.SelectedItem)?.Id ?? -1;
            Patient.CityId =   ((TblCity) EdTxtCity.SelectedItem)?.Id ?? -1;

            this.Close();
            Result = MessageBoxResult.OK;
        }

        private void RadioPatient_OnChecked(object sender, RoutedEventArgs e)
        {
            if (WrpHelper == null) return;
            WrpHelper.Visibility = Visibility.Collapsed;
            WrpHospital.Visibility = Visibility.Collapsed;
            WrpPatient.Visibility = Visibility.Visible;
        }

        private void RadioHelper_OnChecked(object sender, RoutedEventArgs e)
        {
            if (WrpHelper == null) return;
            WrpHelper.Visibility = Visibility.Visible;
            WrpHospital.Visibility = Visibility.Collapsed;
            WrpPatient.Visibility = Visibility.Collapsed;
        }

        private void RadioHospital_OnChecked(object sender, RoutedEventArgs e)
        {
            if (WrpHelper == null) return;
            WrpHelper.Visibility = Visibility.Collapsed;
            WrpHospital.Visibility = Visibility.Visible;
            WrpPatient.Visibility = Visibility.Collapsed;
        }
    }
}