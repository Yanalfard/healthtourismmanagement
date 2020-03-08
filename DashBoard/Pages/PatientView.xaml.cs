using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DashBoard.ApiDecoder;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;
using HelthTourismServer.ApiDecoder;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class PatientView : Page
    {
        const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        const string Username = "yanal";

        public MainWindow MainWindow { get; set; }
        public TblUserPass LoggedInUser { get; set; }

        public PatientView()
        {
            InitializeComponent();
        }

        public PatientView(MainWindow mainWindow, TblUserPass loggedInUser)
        {
            this.MainWindow = mainWindow;
            this.LoggedInUser = loggedInUser;

            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowPreloader();
            foreach (PersonViewer control in await GeneratePatients())
            {
                switch (control.Patient.Status)
                {
                    case (int)PatientStatus.Registered:
                        WrpRegistered.Children.Add(control);
                        break;
                    case (int)PatientStatus.Entered:
                        WrpEntered.Children.Add(control);
                        break;
                    case (int)PatientStatus.Reception:
                        WrpReception.Children.Add(control);
                        break;
                    case (int)PatientStatus.Served:
                        WrpServed.Children.Add(control);
                        break;
                }
            }
            MainWindow.HidePreloader();
        }

        private async Task<List<PersonViewer>> GeneratePatients()
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            PatientCore patientCore = new PatientCore(token);
            CountryCore countryCore = new CountryCore(token);
            CityCore cityCore = new CityCore(token);
            UserPassCore userPassCore = new UserPassCore(token);

            List<PersonViewer> ans = new List<PersonViewer>();
            foreach (TblPatient patient in MainWindow.ArrPatient)
            {
                //Issue#2
                DtoTblCountry country = await countryCore.SelectCountryById(patient.CountryId);
                DtoTblCity city = await cityCore.SelectCityById(patient.CityId);
                DtoTblUserPass userPass = await userPassCore.SelectUserPassById(patient.UserPassId);

                PersonViewer personViewer = new PersonViewer(patient
                    , new TblCountry(country.Id, country.Name)
                    , new TblCity(city.Id, city.Name, city.CountryId)
                    , new TblUserPass(userPass.Id, userPass.Username, userPass.Password, userPass.IsHelthApple));
                ans.Add(personViewer);
            }
            return ans;
        }

        private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            WrpRegistered.Children.Clear();
            WrpEntered.Children.Clear();
            WrpReception.Children.Clear();
            WrpServed.Children.Clear();
            Page_Loaded(sender, null);
        }

        private void PersonViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PersonViewer viewer = sender as PersonViewer;
            MainWindow.CollapseFrames();
            MainWindow.FrameTemp.Content = new PatientTreeView(MainWindow, viewer.Patient, viewer.Country, viewer.City, viewer.UserPass);
            MainWindow.FrameTemp.Visibility = Visibility.Visible;
        }

        #region Carrets
        private void BtnCarretRegistered_OnClick(object sender, RoutedEventArgs e)
        {
            if (WrpRegistered.Visibility == Visibility.Visible)
            {
                BtnCarretRegistered.RenderTransform = new RotateTransform(90);
                WrpRegistered.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnCarretRegistered.RenderTransform = new RotateTransform(180);
                WrpRegistered.Visibility = Visibility.Visible;
            }
        }

        private void BtnCarretEntered_OnClick(object sender, RoutedEventArgs e)
        {
            if (WrpEntered.Visibility == Visibility.Visible)
            {
                BtnCarretEntered.RenderTransform = new RotateTransform(90);
                WrpEntered.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnCarretEntered.RenderTransform = new RotateTransform(180);
                WrpEntered.Visibility = Visibility.Visible;
            }
        }

        private void BtnCarretReception_OnClick(object sender, RoutedEventArgs e)
        {
            if (WrpReception.Visibility == Visibility.Visible)
            {
                BtnCarretReception.RenderTransform = new RotateTransform(90);
                WrpReception.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnCarretReception.RenderTransform = new RotateTransform(180);
                WrpReception.Visibility = Visibility.Visible;
            }
        }

        private void BtnCarretServed_OnClick(object sender, RoutedEventArgs e)
        {
            if (WrpServed.Visibility == Visibility.Visible)
            {
                BtnCarretServed.RenderTransform = new RotateTransform(90);
                WrpServed.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnCarretServed.RenderTransform = new RotateTransform(180);
                WrpServed.Visibility = Visibility.Visible;
            }
        }

        #endregion

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Control ctrl in WrpRegistered.Children)
            {
                PersonViewer card = ctrl as PersonViewer;
                card.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnPerson_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnHospital_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonSearch_OnClick(object sender, RoutedEventArgs e)
        {
            SearchFilter searchFilter = new SearchFilter(this);
            searchFilter.ShowDialog();
            if (searchFilter.Result != MessageBoxResult.OK) return;

            foreach (Control ctrl in WrpRegistered.Children)
            {
                //SearchFilter < Cpatient
                ctrl.Visibility = Visibility.Collapsed;
                PersonViewer viewer = ctrl as PersonViewer;
                TblCountry Ccountry = viewer.Country;
                TblCity Ccity = viewer.City;
                TblPatient Cpatient = viewer.Patient;
                TblHospital Chospital = viewer.Hospital;

                //○ and -1 : You shouldn't check these values
                //○ == alt + 9

                //☺
                //if(Cpatient.IsMan != searchFilter.Patient.IsMan) continue;
                if (!Ccountry.Name.Contains(searchFilter.Country.Name)
                    && searchFilter.Country.Name != "○") continue;
                if (!Ccity.Name.Contains(searchFilter.Name)
                    && searchFilter.Name != "○") continue;

                if (!Cpatient.Name.Contains(searchFilter.Patient.Name)
                    && searchFilter.Patient.Name != "○") continue;
                if (!Cpatient.PassNoOrIdentification.Contains(searchFilter.Patient.PassNoOrIdentification)
                    && searchFilter.Patient.PassNoOrIdentification != "○") continue;
                if (!Cpatient.CoShare.ToString().Contains(searchFilter.Patient.CoShare.ToString())
                    && searchFilter.Patient.CoShare != -1) continue;
                if (!Cpatient.Address.Contains(searchFilter.Patient.Address)
                    && searchFilter.Patient.Address != "○") continue;
                if (!Cpatient.BirthDate.Contains(searchFilter.Patient.BirthDate)
                    && searchFilter.Patient.BirthDate != "○") continue;
                if (!Cpatient.DateReleased.Contains(searchFilter.Patient.DateReleased)
                    && searchFilter.Patient.DateReleased != "○") continue;
                if (!Cpatient.Email.Contains(searchFilter.Patient.DateReleased)
                    && Cpatient.Email != "○") continue;
                if (!Cpatient.HelthCode.Contains(searchFilter.Patient.HelthCode)
                    && Cpatient.HelthCode != "○") continue;
                if (!Cpatient.TellNo.Contains(searchFilter.Patient.TellNo)
                    && Cpatient.TellNo != "○") continue;
                if (!Cpatient.Payed.ToString().Contains(searchFilter.Patient.Payed.ToString())
                    && Cpatient.Payed != -1) continue;
                if (!Cpatient.TimeRegistered.Contains(searchFilter.Patient.TimeRegistered)
                    && Cpatient.TimeRegistered != "○") continue;
                if (!Cpatient.ParentalName.Contains(searchFilter.Patient.ParentalName)
                    && Cpatient.ParentalName != "○") continue;
                if (!Cpatient.Job.Contains(searchFilter.Patient.Job)
                    && Cpatient.Job != "○") continue;
                if (!Cpatient.Insurance.Contains(searchFilter.Patient.Insurance)
                    && Cpatient.Insurance != "○") continue;
                if (!Cpatient.HelpName.Contains(searchFilter.Patient.HelpName)
                    && Cpatient.HelpName != "○") continue;
                if (!Cpatient.HelpJob.Contains(searchFilter.Patient.HelpJob)
                    && Cpatient.HelpJob != "○") continue;
                if (!Cpatient.HelpPassNo.Contains(searchFilter.Patient.HelpPassNo)
                    && Cpatient.HelpPassNo != "○") continue;
                if (!Cpatient.HelpTelNo.Contains(searchFilter.Patient.HelpTelNo)
                    && Cpatient.HelpTelNo != "○") continue;

                if (!Chospital.Percentage.ToString().Contains(searchFilter.Hospital.Percentage.ToString())
                    && Chospital.Percentage != -1) continue;
                if (!Chospital.Description.Contains(searchFilter.Hospital.Description)
                    && Chospital.Description != "○") continue;
                if (!Chospital.Longitude.Contains(searchFilter.Hospital.Longitude)
                    && Chospital.Longitude != "○") continue;
                if (!Chospital.Latitude.Contains(searchFilter.Hospital.Latitude)
                    && Chospital.Latitude != "○") continue;


                ctrl.Visibility = Visibility.Visible;
                #region ☺

                //PropertyInfo[] filterProperties = searchFilter.Patient.GetType().GetProperties();
                //PropertyInfo[] cardProperties = Cpatient.GetType().GetProperties();
                //for (int i = 0; i < filterProperties.Length; i++)
                //{
                //    var a = filterProperties[i].GetValue(searchFilter.Patient).ToString();
                //    if (string.IsNullOrEmpty(a)) continue;
                //    if (long.TryParse(a, out var c))
                //        if (c <= 0) continue;
                //    var b = cardProperties[i].GetValue(Cpatient).ToString();
                //    if (!b.Contains(a)) return;
                //}
                //ctrl.Visibility = Visibility.Visible; 

                #endregion
            }
        }
    }
}
