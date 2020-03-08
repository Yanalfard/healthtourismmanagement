using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using DashBoard.ApiDecoder;
using DashBoard.Models.Regular;
using HelthTourismServer.ApiDecoder;
using MaterialDesignThemes.Wpf;

namespace DashBoard
{
    /// <summary>
    ///     A page to view, add, remove and edit DataModels
    /// </summary>
    public partial class DataView : Page
    {
        /// <summary>
        ///     ViewModel for this instance of DataView
        /// </summary>
        private readonly DataViewViewModel _dataViewViewModel = new DataViewViewModel();

        /// <summary>
        ///     def ctor
        /// </summary>
        public DataView(MainWindow window, TblUserPass userPass)
        {
            InitializeComponent();
            DataContext = _dataViewViewModel;
            LoggedInUser = userPass ?? new TblUserPass(-1, "DefUser", "DefPassword", false);
            CallerWindow = window;
            ClearEditor();
            CallerWindow.ShowPreloader(CallerWindow.BrdPreLoader);
            ArrCity.CollectionChanged += CityOnListChanged;
            ArrCountry.CollectionChanged += CountryOnListChanged;
            ArrSections.CollectionChanged += SectionsOnListChanged;
            ArrHospital.CollectionChanged += HospitalOnCollectionChanged;
            ArrPatient.CollectionChanged += PatientOnCollectionChanged;
            ArrSickness.CollectionChanged += SicknessOnCollectionChanged;
            ArrDoctor.CollectionChanged += DoctorOnCollectionChanged;
            ArrImage.CollectionChanged += ImageOnCollectionChanged;
            ArrOperations.CollectionChanged += OperationsOnCollectionChanged;
            ArrUserPass.CollectionChanged += UserPassOnCollectionChanged;
            SelectedMmDataBlocks.CollectionChanged += SelectedMmDataBlocksOnListChanged;
        }

        private void SelectedMmDataBlocksOnListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            #region Handle Eidtor layout for multiselection

            //Nothing is selected
            if (SelectedMmDataBlocks.Count == 0)
            {
                EdtxtSelected.Text = "موردی انتخاب نشده است";

                //Empty the Editor
                ClearEditor();
                return;
            }
            //A single item is selected
            if (SelectedMmDataBlocks.Count == 1)
            {
                EdtxtSelected.Text = $"{SelectedMmDataBlocks.Count} مورد انتخاب شده است";
                EdtxtSelected.Visibility = Visibility.Collapsed;

                EdBtnEdit.Visibility = Visibility.Visible;
                EdBtnDelete.Visibility = Visibility.Visible;

            }
            //Multiple Items are selected
            else if (SelectedMmDataBlocks.Count > 1)
            {
                NeutFields();
                EdtxtSelected.Text = $"{SelectedMmDataBlocks.Count} مورد انتخاب شده است";

                #region ColorAnimation

                var colorChangeAnimation = new ColorAnimation(
                    Color.FromRgb(190, 190, 190),
                    Color.FromRgb(160, 160, 160),
                    TimeSpan.FromMilliseconds(320));
                var cellBackgroundChangeStory = new Storyboard();
                Storyboard.SetTarget(colorChangeAnimation, EdtxtSelected);
                Storyboard.SetTargetProperty(colorChangeAnimation,
                    new PropertyPath("(TextBlock.Background).(SolidColorBrush.Color)"));
                cellBackgroundChangeStory.Children.Add(colorChangeAnimation);
                cellBackgroundChangeStory.Begin();

                #endregion

                EdtxtSelected.Visibility = Visibility.Visible;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SetUpEditor((MetaMorphicDataBlock)e.NewItems[e.NewItems.Count - 1]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    SetUpEditor((MetaMorphicDataBlock)e.OldItems[e.OldItems.Count - 1]);

                    break;
            }

            #endregion
        }
        #region PrepareEditorLayout

        private void NeutFields()
        {
            EdtxtAddress.Text = string.Empty;
            EdtxtAmountPayed.Text = string.Empty;
            EdtxtCoShare.Text = string.Empty;
            EdtxtDescription.Text = string.Empty;
            EdtxtEmail.Text = string.Empty;
            EdtxtHelpPassNo.Text = string.Empty;
            EdtxtHelpJob.Text = string.Empty;
            EdtxtHelpName.Text = string.Empty;
            EdtxtHelpTelNo.Text = string.Empty;
            EdtxtHealthCode.Text = string.Empty;
            EdtxtInsurance.Text = string.Empty;
            EdtxtJob.Text = string.Empty;
            EdtxtLongtitude.Text = string.Empty;
            EdtxtLatitude.Text = string.Empty;
            EdtxtName.Text = string.Empty;
            EdtxtParentalName.Text = string.Empty;
            EdtxtPassNoOrIdentification.Text = string.Empty;
            EdtxtPassword.Text = string.Empty;
            EdtxtPercentage.Text = string.Empty;
            EdtxtPrice.Text = string.Empty;
            EdtxtTellNumber.Text = string.Empty;
            EdtxtUserPassId.Text = string.Empty;
        }

        private void SetUpEditor(MetaMorphicDataBlock dataBlock)
        {
            NeutFields();
            if (SelectedMmDataBlocks.Count > 1) return;

            #region PrepareEditorLayout

            switch (dataBlock.GridModelView)
            {
                case GridModelView.Section:
                    ClearEditor(true);
                    SetUpEditorForSection(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Patient:
                    ClearEditor(true);
                    SetUpEditorForPatient(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Hospital:
                    ClearEditor(true);
                    SetUpEditorForHospital(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Sickness:
                    ClearEditor(true);
                    SetUpEditorForSickness(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Doctor:
                    ClearEditor(true);
                    SetUpEditorForDoctor(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Image:
                    ClearEditor(true);
                    SetUpEditorForImage(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Country:
                    ClearEditor(true);
                    SetUpEditorForCountry(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.City:
                    ClearEditor(true);
                    SetUpEditorForCity(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Operation:
                    ClearEditor(true);
                    SetUpEditorForOperation(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.UserPass:
                    ClearEditor(true);
                    SetUpEditorForUserPass(SelectedMmDataBlocks[0]);
                    break;
            }

            #endregion
        }

        private void SetUpEditor(GridModelView modelView)
        {
            NeutFields();
            if (SelectedMmDataBlocks.Count > 1) return;

            switch (modelView)
            {
                case GridModelView.Section:
                    ClearEditor(true);
                    SetUpEditorForSection(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Patient:
                    ClearEditor(true);
                    SetUpEditorForPatient(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Hospital:
                    ClearEditor(true);
                    SetUpEditorForHospital(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Sickness:
                    ClearEditor(true);
                    SetUpEditorForSickness(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Doctor:
                    ClearEditor(true);
                    SetUpEditorForDoctor(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Image:
                    ClearEditor(true);
                    SetUpEditorForImage(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Country:
                    ClearEditor(true);
                    SetUpEditorForCountry(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.City:
                    ClearEditor(true);
                    SetUpEditorForCity(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.Operation:
                    ClearEditor(true);
                    SetUpEditorForOperation(SelectedMmDataBlocks[0]);
                    break;
                case GridModelView.UserPass:
                    ClearEditor(true);
                    SetUpEditorForUserPass(SelectedMmDataBlocks[0]);
                    break;
            }


        }
        #endregion

        #region ArrCollectionChanged
        private void UserPassOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblUserPass UserPass;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    UserPass = (TblUserPass)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.UserPass, UserPass);
                    DataStckUserPass.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    UserPass = (TblUserPass)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    UserPass = (TblUserPass)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }

        private void OperationsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblOperation Operation;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Operation = (TblOperation)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Operation, Operation);
                    DataStckOperation.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Operation = (TblOperation)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Operation = (TblOperation)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }

        private void ImageOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblImage Image;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Image = (TblImage)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Image, Image);
                    DataStckImage.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Image = (TblImage)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Image = (TblImage)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }

        private void DoctorOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblDoctor Doctor;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Doctor = (TblDoctor)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Doctor, Doctor);
                    DataStckDoctor.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Doctor = (TblDoctor)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Doctor = (TblDoctor)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }

        private void SicknessOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            TblSickness Sickness;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Sickness = (TblSickness)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Sickness, Sickness);
                    DataStckSickness.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Sickness = (TblSickness)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Sickness = (TblSickness)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }

        private void PatientOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblPatient Patient;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Patient = (TblPatient)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Patient, Patient);
                    DataStckPatient.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Patient = (TblPatient)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Patient = (TblPatient)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }


        private void HospitalOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblHospital Hospital;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Hospital = (TblHospital)e.NewItems[e.NewItems.Count - 1];
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Hospital, Hospital);
                    DataStckHospital.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Hospital = (TblHospital)e.NewItems[e.NewItems.Count - 1];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Hospital = (TblHospital)e.NewItems[e.NewItems.Count - 1];
                    break;
            }
        }



        private void SectionsOnListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblSection section;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    section = (TblSection)e.NewItems[e.NewItems.Count - 1];
                    EdComboSection.Items.Add(section);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    section = (TblSection)e.NewItems[e.NewItems.Count - 1];
                    EdComboSection.Items.Remove(section);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    section = (TblSection)e.NewItems[e.NewItems.Count - 1];
                    EdComboSection.Items[EdComboSection.Items.IndexOf(section)] = section;
                    break;
            }
        }


        private void LoadEdComboCity()
        {
            foreach (var city in ArrCity)
                EdComboCity.Items.Add(new ComboBoxItem { Tag = city, Content = city.Name });
        }

        private void LoadEdComboCountry()
        {
            foreach (var country in ArrCountry)
                EdComboCountry.Items.Add(new ComboBoxItem { Tag = country, Content = country.Name });
        }

        private void LoadEdComboSection()
        {
            foreach (var section in ArrSections)
                EdComboSection.Items.Add(new ComboBoxItem { Tag = section, Content = section.SectionName });
        }


        private void CountryOnListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblCountry country;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    country = (TblCountry)e.NewItems[e.NewItems.Count - 1];
                    EdComboCountry.Items.Add(new ComboBoxItem { Tag = country, Content = country.Name });
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Country, country);
                    DataStckCountry.Children.Add(block);

                    break;
                case NotifyCollectionChangedAction.Remove:
                    country = (TblCountry)e.NewItems[e.NewItems.Count - 1];
                    foreach (var item in EdComboCountry.Items)
                    {
                        ComboBoxItem citem = (ComboBoxItem)item;
                        if (((TblCountry)citem.Tag).Id == country.Id)
                        {
                            EdComboCountry.Items.Remove(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    country = (TblCountry)e.NewItems[e.NewItems.Count - 1];
                    EdComboCountry.Items[EdComboSection.Items.IndexOf(country)] = country;
                    break;
            }
        }

        private void CityOnListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TblCity city;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    city = (TblCity)e.NewItems[e.NewItems.Count - 1];
                    EdComboCity.Items.Add(city);
                    MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.City, city);
                    DataStckCity.Children.Add(block);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    city = (TblCity)e.NewItems[e.NewItems.Count - 1];
                    EdComboCity.Items.Remove(city);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    city = (TblCity)e.NewItems[e.NewItems.Count - 1];
                    EdComboCity.Items[EdComboCity.Items.IndexOf(city)] = city;
                    break;
            }
        }
        #endregion

        #region Properties

        /// <summary>
        ///     The window that is hosting this page
        /// </summary>
        public MainWindow CallerWindow { get; set; }

        /// <summary>
        ///     The current user that has logged in using the login page
        /// </summary>
        public TblUserPass LoggedInUser { get; set; }


        /// <summary>
        ///     A translated version of <see cref="GridModelView" /> Is compatible with the original
        /// </summary>
        private readonly List<string> _comboBoxTraverseItemMap = new List<string>
        {
            "بخش",
            "بیمار",
            "بیمارستان",
            "بیماری",
            "دکتر",
            "عکس",
            "کشور",
            "شهر",
            "عمل",
            "کاربران"
        };


        /// <summary>
        ///     A list of Currently selected <see cref="MetaMorphicDataBlock" />s
        /// </summary>
        private ObservableCollection<MetaMorphicDataBlock> SelectedMmDataBlocks { get; } = new ObservableCollection<MetaMorphicDataBlock>();

        /// <summary>
        ///     A list of all the DataBlocks that are going to get deleted
        /// </summary>
        //Obsolete
        //public List<MetaMorphicDataBlock> DeletedDataBlocks { get; set; } = new List<MetaMorphicDataBlock>();
        //public StackPanel CurrentDataStack;

        private const string Password = "110ff8d5d2ec47b98b1d53fc3d2bb4e1b517864b502e2f8d1f4cf4b10c017cee";
        private const string Username = "yanal";


        //A list of all the DataModels that are downloaded
        #region Arrived

        public ObservableCollection<TblSection> ArrSections = new ObservableCollection<TblSection>();
        public ObservableCollection<TblPatient> ArrPatient = new ObservableCollection<TblPatient>();
        public ObservableCollection<TblHospital> ArrHospital = new ObservableCollection<TblHospital>();
        public ObservableCollection<TblSickness> ArrSickness = new ObservableCollection<TblSickness>();
        public ObservableCollection<TblDoctor> ArrDoctor = new ObservableCollection<TblDoctor>();
        public ObservableCollection<TblImage> ArrImage = new ObservableCollection<TblImage>();
        public ObservableCollection<TblCountry> ArrCountry = new ObservableCollection<TblCountry>();
        public ObservableCollection<TblCity> ArrCity = new ObservableCollection<TblCity>();
        public ObservableCollection<TblOperation> ArrOperations = new ObservableCollection<TblOperation>();
        public ObservableCollection<TblUserPass> ArrUserPass = new ObservableCollection<TblUserPass>();

        #endregion


        //A list of all the DataModels that are downloaded
        #region DataBlocks

        //public List<MetaMorphicDataBlock> BlocksSection = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksPatient = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksHostpitals = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksSickness = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksDoctor = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksImage = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksCountry = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksCity = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksOperation = new List<MetaMorphicDataBlock>();
        //public List<MetaMorphicDataBlock> BlocksUserPass = new List<MetaMorphicDataBlock>();

        #endregion

        #endregion


        /// <summary>
        ///     Where everything starts
        /// </summary>
        private bool _isExecuted;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.DataContext = _dataViewViewModel;

            GenerateSearchBlocks();
            CollapseDataStacks();

            //first this
            try
            {
                await FetchCountry();
                await FetchCity();
                //await FetchSection();
                //await FetchHospital();
                //await FetchSickness();
                //await FetchDoctor();
                //await FetchImage();
                //await FetchOperation();
                //await FetchUserPass();
                //-not working
                //await FetchPatient();

                _isExecuted = true;

                //await FetchModelData();

            }
            catch (Exception exp)
            {
                new BlurOutMessageBox().Show(exp.Message);
                CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
                return;
            }

            //wait
            while (!_isExecuted) Thread.Sleep(200);

            //CurrentDataStack = dataStckSearchCountry;

            //LoadEdComboCity();
            //LoadEdComboCountry();
            //LoadEdComboSection();
            CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
        }

        private void GenerateSearchBlocks()
        {
            GenerateDataBlocksCity();
            GenerateDataBlocksCountry();
            GenerateDataBlocksDoctor();
            GenerateDataBlocksHospital();
            GenerateDataBlocksImage();
            GenerateDataBlocksOperation();
            GenerateDataBlocksPatient();
            GenerateDataBlocksSection();
            GenerateDataBlocksSickness();
            GenerateDataBlocksUserPass();
        }

        private void GenerateDataBlocksUserPass()
        {
            //UserPass
            var block = new MetaMorphicSearchBlock(GridModelView.UserPass, new TblUserPass());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchUserPass.Children.Add(block);


        }

        private void GenerateDataBlocksSickness()
        {
            //Sickness
            var block = new MetaMorphicSearchBlock(GridModelView.Sickness, new TblSickness());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchSickness.Children.Add(block);
        }

        private void GenerateDataBlocksSection()
        {
            //Section
            var block = new MetaMorphicSearchBlock(GridModelView.Section, new TblSection());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchSection.Children.Add(block);
        }

        private void GenerateDataBlocksPatient()
        {
            //Patient
            var block = new MetaMorphicSearchBlock(GridModelView.Patient, new TblPatient());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Click += chckSearchBlock_Checked;
            DataStckSearchPatient.Children.Add(block);
        }

        private void GenerateDataBlocksOperation()
        {
            //Operation
            var block = new MetaMorphicSearchBlock(GridModelView.Operation, new TblOperation());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchOperation.Children.Add(block);
        }

        private void GenerateDataBlocksImage()
        {
            //Image
            var block = new MetaMorphicSearchBlock(GridModelView.Image, new TblImage());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchImage.Children.Add(block);
        }

        private void GenerateDataBlocksHospital()
        {
            //Hospital
            var block = new MetaMorphicSearchBlock(GridModelView.Hospital, new TblHospital());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchHospital.Children.Add(block);
        }

        private void GenerateDataBlocksDoctor()
        {
            //Doctor
            var block = new MetaMorphicSearchBlock(GridModelView.Doctor, new TblDoctor());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchDoctor.Children.Add(block);
        }

        private void GenerateDataBlocksCountry()
        {
            //Country
            var block = new MetaMorphicSearchBlock(GridModelView.Country, new TblCountry());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchCountry.Children.Add(block);
        }

        private void GenerateDataBlocksCity()
        {
            //City
            var block = new MetaMorphicSearchBlock(GridModelView.City, new TblCity());
            foreach (Control control in block.MainStackPanel.Children)
                if (control is SearchField) ((SearchField)control).BtnSearch.Click += btnSearchField_Click;
                else if (control is SearchDateTime)
                    ((SearchDateTime)control).BtnSearch.Click += btnSearchDateBlock_Click;
                else if (control is SearchCheckBox)
                    ((SearchCheckBox)control).ChckSearch.Checked += chckSearchBlock_Checked;
            DataStckSearchCity.Children.Add(block);
        }

        private void btnSearchField_Click(object sender, RoutedEventArgs e)
        {


            var searchButton = (Button)sender;
            var searchField = (searchButton.Parent as DockPanel).Parent as SearchField;
            var propertyIndex = searchField.PropertyIndex;
            var textBox = (TextBox)((DockPanel)searchButton.Parent).Children[1];
            switch (ComboBoxTraverse.SelectedIndex)
            {
                case (int)GridModelView.Section:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckSection);
                    break;
                case (int)GridModelView.Patient:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckPatient);
                    break;
                case (int)GridModelView.Hospital:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckHospital);
                    break;
                case (int)GridModelView.Sickness:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckSickness);
                    break;
                case (int)GridModelView.Doctor:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckDoctor);
                    break;
                case (int)GridModelView.Image:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckImage);
                    break;
                case (int)GridModelView.Country:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckCountry);
                    break;
                case (int)GridModelView.City:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckCity);
                    break;
                case (int)GridModelView.Operation:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckOperation);
                    break;
                case (int)GridModelView.UserPass:
                    FilterMmDataBlocks(propertyIndex, textBox.Text, DataStckUserPass);
                    break;
            }
        }


        /// <summary>
        ///     A list of all the datablocks that are hidden by a filter Method/>
        /// </summary>
        private readonly List<MetaMorphicDataBlock> _hiddenDataBlocks = new List<MetaMorphicDataBlock>();

        /// <summary>
        ///     Filters the <see cref="MetaMorphicDataBlock" />s in a <see cref="StackPanel" />
        /// </summary>
        /// <param name="propertyIndex">which property to compare</param>
        /// <param name="clue">what text to compare</param>
        /// <param name="stackPanel">where the <see cref="MetaMorphicDataBlock" />s are</param>
        private void FilterMmDataBlocks(int propertyIndex, string clue, StackPanel stackPanel)
        {
            foreach (Control ctrl in stackPanel.Children)
            {
                var dataBlock = (MetaMorphicDataBlock)ctrl;
                var property = dataBlock.DataModel.GetType().GetProperties()[propertyIndex];
                var value = property.GetValue(dataBlock.DataModel).ToString();
                dataBlock.Visibility = Visibility.Visible;
                if (!value.Contains(clue))
                {
                    dataBlock.Visibility = Visibility.Collapsed;
                    _hiddenDataBlocks.Add(dataBlock);
                }
            }
        }


        private void btnSearchDateBlock_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void chckSearchBlock_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == null)
            {
                foreach (var dataBlock in _hiddenDataBlocks) dataBlock.Visibility = Visibility.Visible;
                _hiddenDataBlocks.Clear();
                return;
            }

            var searchField = ((DockPanel)checkBox.Parent).Parent as SearchCheckBox;
            var propertyIndex = searchField.PropertyIndex;
            switch (ComboBoxTraverse.SelectedIndex)
            {
                case (int)GridModelView.Section:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckSection);
                    break;
                case (int)GridModelView.Patient:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckPatient);
                    break;
                case (int)GridModelView.Hospital:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckHospital);
                    break;
                case (int)GridModelView.Sickness:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckSickness);
                    break;
                case (int)GridModelView.Doctor:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckDoctor);
                    break;
                case (int)GridModelView.Image:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckImage);
                    break;
                case (int)GridModelView.Country:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckCountry);
                    break;
                case (int)GridModelView.City:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckCity);
                    break;
                case (int)GridModelView.Operation:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckOperation);
                    break;
                case (int)GridModelView.UserPass:
                    FilterMmDataBlocks(propertyIndex, (bool)checkBox.IsChecked, DataStckUserPass);
                    break;
            }
        }

        /// <summary>
        ///     Filters the <see cref="MetaMorphicDataBlock" />s in a <see cref="StackPanel" />
        /// </summary>
        /// <param name="propertyIndex">which property to compare</param>
        /// <param name="clue">if the parameter is checked or not</param>
        /// <param name="stackPanel">where the <see cref="MetaMorphicDataBlock" />s are</param>
        private void FilterMmDataBlocks(int propertyIndex, bool clue, StackPanel stackPanel)
        {
            foreach (Control ctrl in stackPanel.Children)
            {
                var dataBlock = (MetaMorphicDataBlock)ctrl;
                var property = dataBlock.DataModel.GetType().GetProperties()[propertyIndex];
                var value = bool.Parse(property.GetValue(dataBlock.DataModel).ToString());
                dataBlock.Visibility = Visibility.Visible;
                if (value != clue)
                {
                    dataBlock.Visibility = Visibility.Collapsed;
                    _hiddenDataBlocks.Add(dataBlock);
                }
            }
        }

        #region DataFetchers

        /// <summary>
        ///     Gets all the DataModels from the server to later view to the client
        /// </summary>
        private async Task FetchModelData()
        {

            await FetchCountry();
            await FetchCity();
            await FetchSection();
            await FetchPatient();
            await FetchHospital();
            await FetchSickness();
            await FetchDoctor();
            await FetchImage();
            //Select all operation in operation Core Returns Null
            await FetchOperation();
            await FetchUserPass();

            _isExecuted = true;
        }

        private async Task FetchUserPass()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var userPassCore = new UserPassCore(token);
            var dtoTblUserPass = await userPassCore.SelectAllUserPasss();
            foreach (var userPass in dtoTblUserPass)
                if (CheckHttpAccessCode(userPass.StatusEffect))
                    ArrUserPass.Add(new TblUserPass(userPass.Id, userPass.Username, userPass.Password,
                        userPass.IsHelthApple));
        }

        private async Task FetchDoctor()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var doctorCore = new DoctorCore(token);
            var dtoTblDoctors = await doctorCore.SelectAllDoctors();
            foreach (var doctor in dtoTblDoctors)
                if (CheckHttpAccessCode(doctor.StatusEffect))
                    ArrDoctor.Add(new TblDoctor(doctor.Id, doctor.Name, doctor.SectionId, doctor.NowActive));
        }

        private async Task FetchOperation()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var operationCore = new OperationCore(token);
            var dtoTblOperations = await operationCore.SelectAllOperations();
            foreach (var operation in dtoTblOperations)
                if (CheckHttpAccessCode(operation.StatusEffect))
                    ArrOperations.Add(new TblOperation(operation.Id, operation.OperationName,
                        operation.OperationPrice));
        }

        private async Task FetchImage()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var imageCore = new ImageCore(token);
            var dtoTblImages = await imageCore.SelectAllImages();
            foreach (var image in dtoTblImages)
                if (CheckHttpAccessCode(image.StatusEffect))
                    ArrImage.Add(new TblImage(image.Id, image.Image, image.Status));
        }

        private async Task FetchSickness()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sicknessCore = new SicknessCore(token);
            var dtoTblSicknesses = await sicknessCore.SelectAllSicknesss();
            foreach (var sickness in dtoTblSicknesses)
                if (CheckHttpAccessCode(sickness.StatusEffect))
                    ArrSickness.Add(new TblSickness(sickness.Id, sickness.Name, sickness.SectionId));
        }

        private async Task FetchHospital()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var hospitalCore = new HospitalCore(token);
            var dtoTblHospitals = await hospitalCore.SelectAllHospitals();
            foreach (var hospital in dtoTblHospitals)
                if (CheckHttpAccessCode(hospital.StatusEffect))
                    ArrHospital.Add(new TblHospital(hospital.Id, hospital.Name, hospital.UserPassId,
                        hospital.Percentage, hospital.Description, hospital.Longitude,
                        hospital.Latitude));
        }

        private async Task FetchPatient()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var patientCore = new PatientCore(token);
            var dtoTblPatients = await patientCore.SelectAllPatients();
            foreach (var patient in dtoTblPatients)
                if (CheckHttpAccessCode(patient.StatusEffect))
                    ArrPatient.Add(new TblPatient(patient.Id, patient.Name, patient.IsMan, patient.BirthDate,
                        patient.CountryId, patient.CityId, patient.PassNoOrIdentification, patient.HelthCode,
                        patient.Email, patient.TellNo, patient.Address, patient.Payed, patient.CoShare,
                        patient.DateReleased, patient.UserPassId, patient.Status, patient.TimeRegistered,
                        patient.ParentalName, patient.Job
                        , patient.Insurance, patient.HelpName, patient.HelpJob, patient.HelpPassNo, patient.HelpTelNo));
        }

        private async Task FetchSection()
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sectionCore = new SectionCore(token);
            var dtoTblSections = await sectionCore.SelectAllSections();
            foreach (var section in dtoTblSections)
                if (CheckHttpAccessCode(section.StatusEffect))
                    ArrSections.Add(new TblSection(section.Id, section.SectionName));
        }

        private async Task FetchCity()
        {
            //City
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var cityCore = new CityCore(token);
            var dtoTblCities = await cityCore.SelectAllCitys();
            foreach (var city in dtoTblCities)
                if (CheckHttpAccessCode(city.StatusEffect))
                    ArrCity.Add(new TblCity(city.Id, city.Name, city.CountryId));
        }

        private async Task FetchCountry()
        {
            //Country
            SecurityCore securityCore = new SecurityCore();
            string token = null;
            while (token == null) token = await securityCore.GenerateToken(Username, Password);
            var countryCore = new CountryCore(token);
            var dtoTblCountries = await countryCore.SelectAllCountrys();
            foreach (var country in dtoTblCountries)
                if (CheckHttpAccessCode(country.StatusEffect))
                    ArrCountry.Add(new TblCountry(country.Id, country.Name));
        }

        public bool CheckHttpAccessCode(HttpStatusCode httpStatus)
        {
            try
            {
                switch (httpStatus)
                {
                    //case HttpStatusCode.Ambiguous:
                    //    break;
                    //case HttpStatusCode.Moved:
                    //    break;
                    //case HttpStatusCode.Redirect:
                    //    break;
                    //case HttpStatusCode.RedirectMethod:
                    //    break;
                    //case HttpStatusCode.RedirectKeepVerb:
                    //    break;
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.Continue:
                        throw new Exception("Continue");
                    case HttpStatusCode.SwitchingProtocols:
                        throw new Exception("SwitchingProtocols");
                    case HttpStatusCode.Created:
                        throw new Exception("Created");
                    case HttpStatusCode.Accepted:
                        throw new Exception("Accepted");
                    case HttpStatusCode.NonAuthoritativeInformation:
                        throw new Exception("NonAuthoritativeInformation");
                    case HttpStatusCode.NoContent:
                        throw new Exception("NoContent");
                    case HttpStatusCode.ResetContent:
                        throw new Exception("ResetContent");
                    case HttpStatusCode.PartialContent:
                        throw new Exception("PartialContent");
                    case HttpStatusCode.MultipleChoices:
                        throw new Exception("MultipleChoices");
                    case HttpStatusCode.MovedPermanently:
                        throw new Exception("MovedPermanently");
                    case HttpStatusCode.Found:
                        throw new Exception("Found");
                    case HttpStatusCode.SeeOther:
                        throw new Exception("SeeOther");
                    case HttpStatusCode.NotModified:
                        throw new Exception("NotModified");
                    case HttpStatusCode.UseProxy:
                        throw new Exception("UseProxy");
                    case HttpStatusCode.Unused:
                        throw new Exception("Exception");
                    case HttpStatusCode.TemporaryRedirect:
                        throw new Exception("TemporaryRedirect");
                    case HttpStatusCode.BadRequest:
                        throw new Exception("BadRequest");
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("Unauthorized");
                    case HttpStatusCode.PaymentRequired:
                        throw new Exception("PaymentRequired");
                    case HttpStatusCode.Forbidden:
                        throw new Exception("Forbidden");
                    case HttpStatusCode.NotFound:
                        throw new Exception("NotFound");
                    case HttpStatusCode.MethodNotAllowed:
                        throw new Exception("MethodNotAllowed");
                    case HttpStatusCode.NotAcceptable:
                        throw new Exception("NotAcceptable");
                    case HttpStatusCode.ProxyAuthenticationRequired:
                        throw new Exception("ProxyAuthenticationRequired");
                    case HttpStatusCode.RequestTimeout:
                        throw new Exception("RequestTimeout");
                    case HttpStatusCode.Conflict:
                        throw new Exception("Conflict");
                    case HttpStatusCode.Gone:
                        throw new Exception("Gone");
                    case HttpStatusCode.LengthRequired:
                        throw new Exception("LengthRequired");
                    case HttpStatusCode.PreconditionFailed:
                        throw new Exception("PreconditionFailed");
                    case HttpStatusCode.RequestEntityTooLarge:
                        throw new Exception("RequestEntityTooLarge");
                    case HttpStatusCode.RequestUriTooLong:
                        throw new Exception("RequestUriTooLong");
                    case HttpStatusCode.UnsupportedMediaType:
                        throw new Exception("UnsupportedMediaType");
                    case HttpStatusCode.RequestedRangeNotSatisfiable:
                        throw new Exception("RequestedRangeNotSatisfiable");
                    case HttpStatusCode.ExpectationFailed:
                        throw new Exception("ExpectationFailed");
                    case HttpStatusCode.UpgradeRequired:
                        throw new Exception("UpgradeRequired");
                    case HttpStatusCode.InternalServerError:
                        throw new Exception("InternalServerError");
                    case HttpStatusCode.NotImplemented:
                        throw new Exception("NotImplemented");
                    case HttpStatusCode.BadGateway:
                        throw new Exception("BadGateway");
                    case HttpStatusCode.ServiceUnavailable:
                        throw new Exception("ServiceUnavailable");
                    case HttpStatusCode.GatewayTimeout:
                        throw new Exception("GatewayTimeout");
                    case HttpStatusCode.HttpVersionNotSupported:
                        throw new Exception("HttpVersionNotSupported");
                    default:
                        return false;
                }
            }
            catch (Exception exception)
            {
                //If an error happened
                MessageBox.Show(exception.Message);
                CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
                return false;
            }
        }

        #endregion

        #region DataViewSelection

        /// <summary>
        ///     Setup Comboboxes items.
        /// </summary>
        private void ComboBoxTraverse_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxTraverse.ItemsSource = _comboBoxTraverseItemMap;
            ComboBoxTraverse.SelectedIndex = (int)GridModelView.Country;
        }

        private int _comboboxSelectedIndex = (int)GridModelView.Country;

        /// <summary>
        ///     Run When ComboBoxSelection is changed
        /// </summary>
        private void ComboBoxTraverse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTraverse.SelectedIndex == _comboboxSelectedIndex) return;

            if (SelectedMmDataBlocks.Count > 0)
                switch (new BlurOutMessageBox().Show("هشدار", "تغییرات در این صفحه از بین خواهد رفت. آیا مطمئن هستید؟",
                    "بله", "خیر", "لغو"))
                {
                    case MessageBoxResult.Cancel:
                        e.Handled = true;
                        ComboBoxTraverse.SelectedItem = e.RemovedItems[0];
                        return;
                    case MessageBoxResult.Yes:
                        ClearSelectedMmDataBlocks();
                        break;
                    case MessageBoxResult.No:
                        e.Handled = true;
                        ComboBoxTraverse.SelectedItem = e.RemovedItems[0];
                        return;
                }
            else if (_isAdding)
                switch (new BlurOutMessageBox().Show("هشدار",
                    "داده در حال اضافه شدن است آیا مطمئن هستید میخواهید صفحه را ترک کنید؟  ", "بله", "خیر", "لغو"))
                {
                    case MessageBoxResult.Cancel:
                        e.Handled = false;
                        ComboBoxTraverse.SelectedItem = e.RemovedItems[0];
                        return;
                    case MessageBoxResult.Yes:
                        ClearSelectedMmDataBlocks();
                        ClearEditor();
                        ((StackPanel)_birthingMmDataBlock.Parent).Children.Remove(_birthingMmDataBlock);
                        _isAdding = false;
                        BtnAdd.Content = new PackIcon
                        {
                            Kind = PackIconKind.Add,
                            Width = 32,
                            Height = 32,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        break;
                    case MessageBoxResult.No:
                        e.Handled = false;
                        ComboBoxTraverse.SelectedItem = e.RemovedItems[0];
                        return;
                }

            _comboboxSelectedIndex = ComboBoxTraverse.SelectedIndex;

            switch (ComboBoxTraverse.SelectedIndex)
            {
                case (int)GridModelView.City:
                    CollapseDataStacks();
                    //CurrentDataStack = dataStckCity;
                    DataStckCity.Visibility = Visibility.Visible;
                    DataStckSearchCity.Visibility = Visibility.Visible;
                    DockCity.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Country:
                    //CurrentDataStack = dataStckDoctor;
                    CollapseDataStacks();
                    DataStckCountry.Visibility = Visibility.Visible;
                    DataStckSearchCountry.Visibility = Visibility.Visible;
                    DockCountry.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Doctor:
                    //CurrentDataStack = dataStckDoctor;
                    CollapseDataStacks();
                    DataStckDoctor.Visibility = Visibility.Visible;
                    DataStckSearchDoctor.Visibility = Visibility.Visible;
                    DockDoctor.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Hospital:
                    //CurrentDataStack = dataStckHospital;
                    CollapseDataStacks();
                    DataStckHospital.Visibility = Visibility.Visible;
                    DataStckSearchHospital.Visibility = Visibility.Visible;
                    DockHospital.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Image:
                    //CurrentDataStack = dataStckImage;
                    CollapseDataStacks();
                    DataStckImage.Visibility = Visibility.Visible;
                    DataStckSearchImage.Visibility = Visibility.Visible;
                    DockImage.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Operation:
                    //CurrentDataStack = dataStckOperation;
                    CollapseDataStacks();
                    DataStckOperation.Visibility = Visibility.Visible;
                    DataStckSearchOperation.Visibility = Visibility.Visible;
                    DockOperation.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Patient:
                    //CurrentDataStack = dataStckPatient;
                    CollapseDataStacks();
                    DataStckPatient.Visibility = Visibility.Visible;
                    DataStckSearchPatient.Visibility = Visibility.Visible;
                    DockPatient.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Section:
                    //CurrentDataStack = dataStckSection;
                    CollapseDataStacks();
                    DataStckSection.Visibility = Visibility.Visible;
                    DataStckSearchSection.Visibility = Visibility.Visible;
                    DockSection.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.Sickness:
                    //CurrentDataStack = dataStckSickness;
                    CollapseDataStacks();
                    DataStckSickness.Visibility = Visibility.Visible;
                    DataStckSearchSickness.Visibility = Visibility.Visible;
                    DockSickness.Visibility = Visibility.Visible;
                    break;
                case (int)GridModelView.UserPass:
                    //CurrentDataStack = dataStckUserPass;
                    CollapseDataStacks();
                    DataStckUserPass.Visibility = Visibility.Visible;
                    DataStckSearchUserPass.Visibility = Visibility.Visible;
                    DockUserPass.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ClearSelectedMmDataBlocks()
        {
            foreach (var item in SelectedMmDataBlocks) item.ChkboxSelection.IsChecked = false;
            SelectedMmDataBlocks.Clear();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private void CollapseDataStacks()
        {
            DataStckCity.Visibility = Visibility.Collapsed;
            DataStckSearchCity.Visibility = Visibility.Collapsed;
            DockCity.Visibility = Visibility.Collapsed;
            DataStckCountry.Visibility = Visibility.Collapsed;
            DataStckSearchCountry.Visibility = Visibility.Collapsed;
            DockCountry.Visibility = Visibility.Collapsed;
            DataStckDoctor.Visibility = Visibility.Collapsed;
            DataStckSearchDoctor.Visibility = Visibility.Collapsed;
            DockDoctor.Visibility = Visibility.Collapsed;
            DataStckHospital.Visibility = Visibility.Collapsed;
            DataStckSearchHospital.Visibility = Visibility.Collapsed;
            DockHospital.Visibility = Visibility.Collapsed;
            DataStckImage.Visibility = Visibility.Collapsed;
            DataStckSearchImage.Visibility = Visibility.Collapsed;
            DockImage.Visibility = Visibility.Collapsed;
            DataStckOperation.Visibility = Visibility.Collapsed;
            DataStckSearchOperation.Visibility = Visibility.Collapsed;
            DockOperation.Visibility = Visibility.Collapsed;
            DataStckPatient.Visibility = Visibility.Collapsed;
            DataStckSearchPatient.Visibility = Visibility.Collapsed;
            DockPatient.Visibility = Visibility.Collapsed;
            DataStckSection.Visibility = Visibility.Collapsed;
            DataStckSearchSection.Visibility = Visibility.Collapsed;
            DockSection.Visibility = Visibility.Collapsed;
            DataStckSickness.Visibility = Visibility.Collapsed;
            DataStckSearchSickness.Visibility = Visibility.Collapsed;
            DockSickness.Visibility = Visibility.Collapsed;
            DataStckUserPass.Visibility = Visibility.Collapsed;
            DataStckSearchUserPass.Visibility = Visibility.Collapsed;
            DockUserPass.Visibility = Visibility.Collapsed;
        }

        #endregion

        private void MetaMorphicDataBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed) return;

            MetaMorphicDataBlock_MouseDown(sender,
                new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            var dataBlock = sender as MetaMorphicDataBlock;
            dataBlock.ChkboxSelection.IsChecked ^= true;
        }

        /// <summary>
        ///     Occures when an MMDataBlock is clicked
        /// </summary>
        private void MetaMorphicDataBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dataBlock = sender as MetaMorphicDataBlock;
            //was it a double click "initiate traversy if so later on"
            var isDoubleClick = e.ClickCount == 2;

            #region Handle MultiSelection

            if (SelectedMmDataBlocks.Contains(dataBlock))
                SelectedMmDataBlocks.Remove(dataBlock);
            else
                SelectedMmDataBlocks.Add(dataBlock);

            #endregion
        }


        //methods in this region set up the editor section found at the right side of the page
        //to edit the approrpiate data 
        #region EditorSetup

        private void SetUpEditorForUserPass(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblUserPass)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Username;
            EdtxtPassword.Visibility = Visibility.Visible;
            EdtxtPassword.Text = dataModel.Password ?? "";
            EdCheckBoxIsHelthApple.Visibility = Visibility.Visible;
            EdCheckBoxIsHelthApple.IsChecked = dataModel.IsHelthApple;
        }

        private void SetUpEditorForOperation(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblOperation)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.OperationName;
            EdtxtPrice.Visibility = Visibility.Visible;
            EdtxtPrice.Text = dataModel.OperationPrice.ToString();
        }


        private void SetUpEditorForCity(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblCity)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
            EdComboCountry.Visibility = Visibility.Visible;
            foreach (var item in EdComboCountry.Items)
            {
                ComboBoxItem citem = (ComboBoxItem)item;
                if (((TblCountry)citem.Tag).Id == dataModel.Id)
                {
                    EdComboCountry.SelectedItem = citem;
                    return;
                }
            }
        }

        private void SetUpEditorForCountry(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblCountry)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
        }


        private void SetUpEditorForImage(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblImage)dataBlock.DataModel;
            EdImage.Visibility = Visibility.Visible;
            EdMainImage.Source = new BitmapImage(new Uri(dataModel.Image ?? ""));
            EdComboStatus.Visibility = Visibility.Visible;
            EdComboStatus.SelectedIndex = dataModel.Status;
        }


        private void SetUpEditorForDoctor(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblDoctor)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
            EdComboSection.Visibility = Visibility.Visible;
            //EdComboSection.ItemsSource = ArrSections;
            EdComboSection.SelectedIndex = dataModel.SectionId;
            EdCheckBoxIsActive.Visibility = Visibility.Visible;
            EdCheckBoxIsActive.IsChecked = dataModel.NowActive;
        }


        private void SetUpEditorForSickness(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblSickness)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
            EdComboSection.Visibility = Visibility.Visible;
            //EdComboSection.ItemsSource = ArrSections;
            EdComboSection.SelectedIndex = dataModel.SectionId;
        }

        private void SetUpEditorForHospital(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblHospital)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
            EdtxtUserPassId.Visibility = Visibility.Visible;
            EdtxtUserPassId.Text = dataModel.UserPassId.ToString();
            EdtxtPercentage.Visibility = Visibility.Visible;
            EdtxtPercentage.Text = dataModel.Percentage.ToString();
            EdtxtDescription.Visibility = Visibility.Visible;
            EdtxtDescription.Text = dataModel.Description;
            EdStckGeo.Visibility = Visibility.Visible;
            EdtxtLongtitude.Visibility = Visibility.Visible;
            EdtxtLongtitude.Text = dataModel.Longitude;
            EdtxtLatitude.Visibility = Visibility.Visible;
            EdtxtLatitude.Text = dataModel.Latitude;
        }


        private void SetUpEditorForPatient(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblPatient)dataBlock.DataModel;
            ClearEditor(true);
            //Id, Name, IsMan, (DateTime)BirthDate, CountryId, CityId,
            // (string)PassNoOrIdentification, (string)HelthCode, Email
            // (string)TellNo, Address, (Long)Payed, (Long)CoShare,
            // (DateTime)DateReleased, UserPassId, (int)Status,(DateTime)TimeRegistered

            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.Name;
            EdStckGender.Visibility = Visibility.Visible;
            EdRadioMale.IsChecked = dataModel.IsMan;
            EdRadioFemale.IsChecked = !dataModel.IsMan;
            EdDatePickerBirthDate.Visibility = Visibility.Visible;
            EdDatePickerBirthDate.Text = dataModel.BirthDate;
            EdComboCountry.Visibility = Visibility.Visible;
            EdComboCountry.SelectedIndex = dataModel.CountryId;
            EdComboCity.Visibility = Visibility.Visible;
            EdComboCity.SelectedIndex = dataModel.CityId;
            EdtxtPassNoOrIdentification.Visibility = Visibility.Visible;
            EdtxtPassNoOrIdentification.Text = dataModel.PassNoOrIdentification;
            EdtxtHealthCode.Visibility = Visibility.Visible;
            EdtxtHealthCode.Text = dataModel.HelthCode;
            EdtxtEmail.Visibility = Visibility.Visible;
            EdtxtEmail.Text = dataModel.Email;
            EdtxtTellNumber.Visibility = Visibility.Visible;
            EdtxtTellNumber.Text = dataModel.TellNo;
            EdtxtAddress.Visibility = Visibility.Visible;
            EdtxtAddress.Text = dataModel.Address;
            EdtxtPrice.Visibility = Visibility.Visible;
            EdtxtPrice.Text = dataModel.Payed.ToString();
            EdtxtPercentage.Visibility = Visibility.Visible;
            EdtxtPercentage.Text = dataModel.CoShare.ToString();
            EdDatePickerReleaseDate.Visibility = Visibility.Visible;
            EdtxtUserPassId.Visibility = Visibility.Visible;
            EdtxtUserPassId.Text = dataModel.UserPassId.ToString();
            EdComboStatus.Visibility = Visibility.Visible;
            EdComboStatus.SelectedIndex = dataModel.Status;
            EdDatePickerTimeRegistered.Visibility = Visibility.Visible;
            EdDatePickerTimeRegistered.Text = dataModel.TimeRegistered;
            EdtxtParentalName.Visibility = Visibility.Visible;
            EdtxtParentalName.Text = dataModel.ParentalName;
            EdtxtInsurance.Visibility = Visibility.Visible;
            EdtxtInsurance.Text = dataModel.Insurance;
            EdtxtJob.Visibility = Visibility.Visible;
            EdtxtJob.Text = dataModel.Job;
            //help
            EdbrdHelp.Visibility = Visibility.Visible;
            EdtxtHelpJob.Text = dataModel.HelpJob;
            EdtxtHelpName.Text = dataModel.HelpName;
            EdtxtHelpPassNo.Text = dataModel.HelpPassNo;
            EdtxtHelpTelNo.Text = dataModel.HelpTelNo;
        }


        private void SetUpEditorForSection(MetaMorphicDataBlock dataBlock)
        {
            var dataModel = (TblSection)dataBlock.DataModel;
            EdtxtName.Visibility = Visibility.Visible;
            EdtxtName.Text = dataModel.SectionName;
        }

        /// <summary>
        ///     Clear all editor components even the Buttons
        /// </summary>
        public void ClearEditor()
        {
            EdCheckBoxIsActive.Visibility = Visibility.Collapsed;
            EdComboCity.Visibility = Visibility.Collapsed;
            EdComboCountry.Visibility = Visibility.Collapsed;
            EdComboImageStatus.Visibility = Visibility.Collapsed;
            EdComboSection.Visibility = Visibility.Collapsed;
            EdComboStatus.Visibility = Visibility.Collapsed;
            EdDatePickerReleaseDate.Visibility = Visibility.Collapsed;
            EdDatePickerTimeRegistered.Visibility = Visibility.Collapsed;
            EdImage.Visibility = Visibility.Collapsed;
            EdStckGender.Visibility = Visibility.Collapsed;
            EdtxtAddress.Visibility = Visibility.Collapsed;
            EdtxtDescription.Visibility = Visibility.Collapsed;
            EdtxtEmail.Visibility = Visibility.Collapsed;
            EdtxtHealthCode.Visibility = Visibility.Collapsed;
            EdStckGeo.Visibility = Visibility.Collapsed;
            EdtxtName.Visibility = Visibility.Collapsed;
            EdtxtPassNoOrIdentification.Visibility = Visibility.Collapsed;
            EdtxtPercentage.Visibility = Visibility.Collapsed;
            EdtxtPrice.Visibility = Visibility.Collapsed;
            EdtxtTellNumber.Visibility = Visibility.Collapsed;
            EdtxtUserPassId.Visibility = Visibility.Collapsed;
            EdtxtCoShare.Visibility = Visibility.Collapsed;
            EdtxtAmountPayed.Visibility = Visibility.Collapsed;
            EdDatePickerBirthDate.Visibility = Visibility.Collapsed;
            EdtxtPassword.Visibility = Visibility.Collapsed;
            EdCheckBoxIsHelthApple.Visibility = Visibility.Collapsed;
            EdBtnAdd.Visibility = Visibility.Collapsed;
            EdBtnEdit.Visibility = Visibility.Collapsed;
            EdBtnDelete.Visibility = Visibility.Collapsed;

            EdbrdHelp.Visibility = Visibility.Collapsed;
            EdtxtParentalName.Visibility = Visibility.Collapsed;
            EdtxtInsurance.Visibility = Visibility.Collapsed;
            EdtxtJob.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///     cleares the editor except the buttons
        /// </summary>
        /// <param name="changeToEdit">hide <see cref="EdBtnAdd" /> and show <see cref="EdBtnEdit" /></param>
        public void ClearEditor(bool changeToEdit)
        {
            if (changeToEdit)
            {
                EdBtnAdd.Visibility = Visibility.Collapsed;
                EdBtnEdit.Visibility = Visibility.Visible;
                EdBtnDelete.Visibility = Visibility.Visible;
            }
            else if (!changeToEdit)
            {
                EdBtnAdd.Visibility = Visibility.Visible;
                EdBtnEdit.Visibility = Visibility.Collapsed;
                EdBtnDelete.Visibility = Visibility.Collapsed;
            }

            EdCheckBoxIsActive.Visibility = Visibility.Collapsed;
            EdComboCity.Visibility = Visibility.Collapsed;
            EdComboCountry.Visibility = Visibility.Collapsed;
            EdComboImageStatus.Visibility = Visibility.Collapsed;
            EdComboSection.Visibility = Visibility.Collapsed;
            EdComboStatus.Visibility = Visibility.Collapsed;
            EdDatePickerReleaseDate.Visibility = Visibility.Collapsed;
            EdDatePickerTimeRegistered.Visibility = Visibility.Collapsed;
            EdImage.Visibility = Visibility.Collapsed;
            EdStckGender.Visibility = Visibility.Collapsed;
            EdtxtAddress.Visibility = Visibility.Collapsed;
            EdtxtDescription.Visibility = Visibility.Collapsed;
            EdtxtEmail.Visibility = Visibility.Collapsed;
            EdtxtHealthCode.Visibility = Visibility.Collapsed;
            EdStckGeo.Visibility = Visibility.Collapsed;
            EdtxtName.Visibility = Visibility.Collapsed;
            EdtxtPassNoOrIdentification.Visibility = Visibility.Collapsed;
            EdtxtPercentage.Visibility = Visibility.Collapsed;
            EdtxtPrice.Visibility = Visibility.Collapsed;
            EdtxtTellNumber.Visibility = Visibility.Collapsed;
            EdtxtUserPassId.Visibility = Visibility.Collapsed;
            EdtxtCoShare.Visibility = Visibility.Collapsed;
            EdtxtAmountPayed.Visibility = Visibility.Collapsed;
            EdDatePickerBirthDate.Visibility = Visibility.Collapsed;
            EdtxtPassword.Visibility = Visibility.Collapsed;
            EdCheckBoxIsHelthApple.Visibility = Visibility.Collapsed;

            EdbrdHelp.Visibility = Visibility.Collapsed;
            EdtxtParentalName.Visibility = Visibility.Collapsed;
            EdtxtInsurance.Visibility = Visibility.Collapsed;
            EdtxtJob.Visibility = Visibility.Collapsed;
        }

        private void EdBtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //ask For confirmation with the amount of selected items provided

            CallerWindow.ShowPreloader(CallerWindow.BrdPreLoader);
            EdBtnDelete.IsEnabled = false;

            foreach (var selectedMm in SelectedMmDataBlocks)
            {
                switch (selectedMm.GridModelView)
                {
                    case GridModelView.Section:
                        DeleteSection((TblSection)selectedMm.DataModel);
                        break;
                    case GridModelView.Patient:
                        DeletePatient((TblPatient)selectedMm.DataModel);
                        break;
                    case GridModelView.Hospital:
                        DeleteHospital((TblHospital)selectedMm.DataModel);
                        break;
                    case GridModelView.Sickness:
                        DeleteSickness((TblSickness)selectedMm.DataModel);
                        break;
                    case GridModelView.Doctor:
                        DeleteDoctor((TblDoctor)selectedMm.DataModel);
                        break;
                    case GridModelView.Image:
                        DeleteImage((TblImage)selectedMm.DataModel);
                        break;
                    case GridModelView.Country:
                        DeleteCountry((TblCountry)selectedMm.DataModel);
                        break;
                    case GridModelView.City:
                        DeleteCity((TblCity)selectedMm.DataModel);
                        break;
                    case GridModelView.Operation:
                        //DeleteOperation((TblOperation)selectedMM.DataModel);
                        break;
                    case GridModelView.UserPass:
                        DeleteUserPass((TblUserPass)selectedMm.DataModel);
                        break;
                }

                //DeletedDataBlocks.Add(selectedMM);
                ((StackPanel)selectedMm.Parent).Children.Remove(selectedMm);
            }

            SelectedMmDataBlocks.Clear();

            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;

            CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
            EdBtnDelete.IsEnabled = true;
        }

        private async void DeleteUserPass(TblUserPass dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var userPassCore = new UserPassCore(token);
            await userPassCore.DeleteUserPass(dataModel.Id);
        }

        private async void DeleteSection(TblSection dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sectionCore = new SectionCore(token);
            await sectionCore.DeleteSection(dataModel.Id);
        }

        private async void DeletePatient(TblPatient dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var patientCore = new PatientCore(token);
            await patientCore.DeletePatient(dataModel.Id);
        }

        private async void DeleteHospital(TblHospital dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var hospitalCore = new HospitalCore(token);
            await hospitalCore.DeleteHospital(dataModel.Id);
        }

        private async void DeleteSickness(TblSickness dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sicknessCore = new SicknessCore(token);
            await sicknessCore.DeleteSickness(dataModel.Id);
        }

        private async void DeleteDoctor(TblDoctor dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var doctorCore = new DoctorCore(token);
            await doctorCore.DeleteDoctor(dataModel.Id);
        }

        private async void DeleteImage(TblImage dataModel)
        {

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var imageCore = new ImageCore(token);
            await imageCore.DeleteImage(dataModel.Id);
        }

        private async void DeleteCountry(TblCountry dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var countryCore = new CountryCore(token);
            await countryCore.DeleteCountry(dataModel.Id);
        }

        private async void DeleteCity(TblCity dataModel)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var cityCore = new CityCore(token);
            await cityCore.DeleteCity(dataModel.Id);
        }

        //private async void DeleteOperation(TblOperation dataModel)
        //{
        //    operation OperationCore = new DoctorOperationCore();
        //    await OperationCore.DeleteDoctorOperation(dataModel.id);
        //}

        private void EdBtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMmDataBlocks.Count == 0) return;

            CallerWindow.ShowPreloader(CallerWindow.BrdPreLoader);
            EdBtnEdit.IsEnabled = false;

            switch (SelectedMmDataBlocks[0].GridModelView)
            {
                case GridModelView.Section:
                    EditSection(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Patient:
                    EditPatient(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Hospital:
                    EditHospital(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Sickness:
                    EditSickness(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Doctor:
                    EditDoctor(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Image:
                    EditImage(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Country:
                    EditCountry(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.City:
                    EditCity(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.Operation:
                    EditOperation(SelectedMmDataBlocks, false);
                    break;
                case GridModelView.UserPass:
                    EditUserPass(SelectedMmDataBlocks, false);
                    break;
            }

            CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
            EdBtnEdit.IsEnabled = true;
        }

        private async void EditUserPass(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool IsAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.UserPass);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }


            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var userPassCore = new UserPassCore(token);
            var newUserPasss = new List<TblUserPass>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldUserPass = (TblUserPass)dataBlock.DataModel;
                var newUserPass = new TblUserPass(
                    oldUserPass.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldUserPass.Username : EdtxtName.Text,
                    string.IsNullOrWhiteSpace(EdtxtPassword.Text) ? oldUserPass.Password : EdtxtPassword.Text,
                    oldUserPass.IsHelthApple
                );
                newUserPasss.Add(newUserPass);
                await userPassCore.UpdateUserPass(newUserPass, oldUserPass.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.UserPass, newUserPass);
                DataStckUserPass.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditOperation(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.City);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            OperationCore OperationCore = new OperationCore(token);
            var newOperations = new List<TblOperation>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldOperation = (TblOperation)dataBlock.DataModel;
                var newOperation = new TblOperation(
                    oldOperation.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldOperation.OperationName : EdtxtName.Text,
                    string.IsNullOrWhiteSpace(EdtxtPrice.Text)
                        ? oldOperation.OperationPrice
                        : long.Parse(EdtxtPrice.Text));
                newOperations.Add(newOperation);
                await OperationCore.UpdateOperation(newOperation, oldOperation.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);


                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Operation, newOperation);
                DataStckOperation.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditCity(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.City);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var cityCore = new CityCore(token);
            var newCitys = new List<TblCity>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldCity = (TblCity)dataBlock.DataModel;
                var newCity = new TblCity(
                    oldCity.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldCity.Name : EdtxtName.Text,

                    string.IsNullOrWhiteSpace(EdComboCountry.Text) ? oldCity.CountryId :
                        ((TblCountry)((ComboBoxItem)EdComboCountry.SelectedItem).Tag).Id
                );
                newCitys.Add(newCity);
                await cityCore.UpdateCity(newCity, oldCity.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);


                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.City, newCity);
                DataStckCity.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditCountry(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Country);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var countryCore = new CountryCore(token);
            var newCountrys = new List<TblCountry>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldCountry = (TblCountry)dataBlock.DataModel;
                var newCountry = new TblCountry(
                    oldCountry.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldCountry.Name : EdtxtName.Text
                );
                newCountrys.Add(newCountry);
                await countryCore.UpdateCountry(newCountry, oldCountry.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Country, newCountry);
                DataStckCountry.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditImage(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Image);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var imageCore = new ImageCore(token);
            var newImages = new List<TblImage>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldImage = (TblImage)dataBlock.DataModel;
                var newImage = new TblImage(
                    oldImage.Id,
                    //??? HOW THE F
                    oldImage.Image,
                    EdComboImageStatus.SelectedIndex
                );
                newImages.Add(newImage);
                await imageCore.UpdateImage(newImage, oldImage.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Image, newImage);
                DataStckImage.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditDoctor(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Doctor);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var doctorCore = new DoctorCore(token);
            var newDoctors = new List<TblDoctor>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldDoctor = (TblDoctor)dataBlock.DataModel;
                var newDoctor = new TblDoctor(
                    oldDoctor.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldDoctor.Name : EdtxtName.Text,
                    string.IsNullOrWhiteSpace(EdComboSection.Text) ? oldDoctor.SectionId :
                        ((TblSection)((ComboBoxItem)EdComboSection.SelectedItem).Tag).Id,
                    oldDoctor.NowActive
                );
                newDoctors.Add(newDoctor);
                await doctorCore.UpdateDoctor(newDoctor, oldDoctor.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Doctor, newDoctor);
                DataStckDoctor.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditSickness(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Sickness);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sicknessCore = new SicknessCore(token);
            var newSicknesss = new List<TblSickness>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldSickness = (TblSickness)dataBlock.DataModel;
                var newSickness = new TblSickness(oldSickness.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldSickness.Name : EdtxtName.Text,
                    string.IsNullOrWhiteSpace(EdComboSection.Text)
                        ? oldSickness.SectionId
                        : ((TblSection)((ComboBoxItem)EdComboSection.SelectedItem).Tag).Id

                );
                newSicknesss.Add(newSickness);
                await sicknessCore.UpdateSickness(newSickness, oldSickness.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Sickness, newSickness);
                DataStckSickness.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditHospital(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Hospital);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            //SecurityCore securityCore = new SecurityCore();
            //string token = await securityCore.GenerateToken(Username, Password);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var hospitalCore = new HospitalCore(token);
            var newHospitals = new List<TblHospital>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldHospital = (TblHospital)dataBlock.DataModel;
                var newHospital = new TblHospital(
                    oldHospital.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldHospital.Name : EdtxtName.Text,
                    string.IsNullOrWhiteSpace(EdtxtUserPassId.Text)
                        ? oldHospital.UserPassId
                        : int.Parse(EdtxtUserPassId.Text),
                    string.IsNullOrWhiteSpace(EdtxtPercentage.Text)
                        ? oldHospital.Percentage
                        : int.Parse(EdtxtPercentage.Text),
                    string.IsNullOrWhiteSpace(EdtxtDescription.Text) ? oldHospital.Description : EdtxtDescription.Text,
                    string.IsNullOrWhiteSpace(EdtxtLongtitude.Text) ? oldHospital.Longitude : EdtxtLongtitude.Text,
                    string.IsNullOrWhiteSpace(EdtxtLatitude.Text) ? oldHospital.Latitude : EdtxtLatitude.Text
                );
                newHospitals.Add(newHospital);
                await hospitalCore.UpdateHospital(newHospital, oldHospital.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Hospital, newHospital);
                DataStckHospital.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditPatient(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Patient);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var patientCore = new PatientCore(token);
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldPatient = (TblPatient)dataBlock.DataModel;
                var newPatient = new TblPatient(
                    oldPatient.Id,
                    string.IsNullOrEmpty(EdtxtName.Text) ? oldPatient.Name : EdtxtName.Text,
                    oldPatient.IsMan,
                    string.IsNullOrEmpty(EdDatePickerBirthDate.Text)
                        ? oldPatient.BirthDate
                        : EdDatePickerBirthDate.Text,
                    string.IsNullOrEmpty(EdComboCountry.Text)
                        ? oldPatient.CountryId
                        : ((TblCountry)EdComboCountry.SelectedItem).Id,
                    string.IsNullOrEmpty(EdComboCity.Text)
                        ? oldPatient.CityId
                        : ((TblCity)EdComboCity.SelectedItem).Id,
                    string.IsNullOrEmpty(EdtxtPassNoOrIdentification.Text)
                        ? oldPatient.PassNoOrIdentification
                        : EdtxtPassNoOrIdentification.Text,
                    string.IsNullOrEmpty(EdtxtHealthCode.Text) ? oldPatient.HelthCode : EdtxtHealthCode.Text,
                    string.IsNullOrEmpty(EdtxtEmail.Text) ? oldPatient.Email : EdtxtEmail.Text,
                    string.IsNullOrEmpty(EdtxtTellNumber.Text) ? oldPatient.TellNo : EdtxtTellNumber.Text,
                    string.IsNullOrEmpty(EdtxtAddress.Text) ? oldPatient.Address : EdtxtAddress.Text,
                    string.IsNullOrEmpty(EdtxtPrice.Text) ? oldPatient.Payed : long.Parse(EdtxtPrice.Text),
                    string.IsNullOrEmpty(EdtxtCoShare.Text) ? oldPatient.CoShare : long.Parse(EdtxtCoShare.Text),
                    string.IsNullOrEmpty(EdDatePickerReleaseDate.Text)
                        ? oldPatient.DateReleased
                        : EdDatePickerReleaseDate.Text,
                    string.IsNullOrEmpty(EdtxtUserPassId.Text)
                        ? oldPatient.UserPassId
                        : int.Parse(EdtxtUserPassId.Text),
                    string.IsNullOrEmpty(EdComboStatus.Text) ? oldPatient.Status : EdComboStatus.SelectedIndex,
                    string.IsNullOrEmpty(EdDatePickerTimeRegistered.Text)
                        ? oldPatient.TimeRegistered
                        : EdDatePickerTimeRegistered.Text,
                    string.IsNullOrEmpty(EdtxtParentalName.Text) ? oldPatient.ParentalName : EdtxtParentalName.Text,
                    string.IsNullOrEmpty(EdtxtJob.Text) ? oldPatient.Job : EdtxtJob.Text,
                    string.IsNullOrEmpty(EdtxtInsurance.Text) ? oldPatient.Insurance : EdtxtInsurance.Text,
                    string.IsNullOrEmpty(EdtxtHelpName.Text) ? oldPatient.HelpName : EdtxtHelpName.Text,
                    string.IsNullOrEmpty(EdtxtHelpJob.Text) ? oldPatient.HelpJob : EdtxtHelpJob.Text,
                    string.IsNullOrEmpty(EdtxtHelpPassNo.Text) ? oldPatient.HelpPassNo : EdtxtHelpPassNo.Text,
                    string.IsNullOrEmpty(EdtxtHelpTelNo.Text) ? oldPatient.HelpTelNo : EdtxtHelpTelNo.Text);
                await patientCore.UpdatePatient(newPatient, oldPatient.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Patient, newPatient);
                DataStckSection.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        private async void EditSection(ObservableCollection<MetaMorphicDataBlock> selectedMmDataBlocks, bool isAdding)
        {
            var response = CheckEditorInputsForEdit(GridModelView.Section);
            if (response != "☺")
            {
                new BlurOutMessageBox().Show("خطا", response, "باشه");
                return;
            }
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sectionCore = new SectionCore(token);
            var newSections = new List<TblSection>();
            foreach (var dataBlock in selectedMmDataBlocks)
            {
                var oldSection = (TblSection)dataBlock.DataModel;
                var newSection = new TblSection(
                    oldSection.Id,
                    string.IsNullOrWhiteSpace(EdtxtName.Text) ? oldSection.SectionName : EdtxtName.Text);
                newSections.Add(newSection);
                await sectionCore.UpdateSection(newSection, oldSection.Id);
                ((StackPanel)dataBlock.Parent).Children.Remove(dataBlock);

                MetaMorphicDataBlock block = new MetaMorphicDataBlock(GridModelView.Section, newSection);
                DataStckSection.Children.Insert(0, block);
                //MetaMorphicDataBlock_MouseDown(block, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            }

            selectedMmDataBlocks.Clear();
            ClearEditor();
            EdtxtSelected.Visibility = Visibility.Collapsed;
        }

        #endregion

        /// <summary>
        ///     A map for patient statuses compatible with <see cref="PatientStatus" />
        /// </summary>
        private readonly List<string> _edComboStatusItemSource = new List<string>
        {
            "ثبت نام شده",
            "در کشور",
            "پذیرش شده",
            "اتمام خدمات"
        };

        /// <summary>
        ///     A map for Image statuses compatible with <see cref="ImageStatus" />
        /// </summary>
        private readonly List<string> _edComboImageStatusItemSource = new List<string>
        {
            "بیمارستان",
            "عمل",
            "اخبار",
            "مدارک بیمار"
        };

        private void LoadEdComboPatientStatus(object sender, RoutedEventArgs e)
        {
            EdComboStatus.ItemsSource = _edComboStatusItemSource;
            EdComboStatus.SelectedIndex = (int)PatientStatus.Reception;
        }

        private void EdComboImageStatus_Loaded(object sender, RoutedEventArgs e)
        {
            EdComboStatus.ItemsSource = _edComboImageStatusItemSource;
            EdComboStatus.SelectedIndex = (int)ImageStatus.Hospital;
        }

        private MetaMorphicDataBlock _birthingMmDataBlock;

        /// <summary>
        ///     If the User is adding a row
        /// </summary>
        private bool _isAdding;

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            #region ClearSelection

            //Clear Off any selection
            foreach (var block in SelectedMmDataBlocks) block.ChkboxSelection.IsChecked = false;
            SelectedMmDataBlocks.Clear();
            EdtxtSelected.Visibility = Visibility.Collapsed;

            #endregion

            ClearEditor(false);

            if (_isAdding) //Cancel Adding
            {
                _isAdding ^= true;
                ClearEditor();

                switch (ComboBoxTraverse.SelectedIndex)
                {
                    case (int)GridModelView.Section:
                        DataStckSection.Children.Remove(DataStckSection.Children[0]);
                        break;
                    case (int)GridModelView.Patient:
                        DataStckPatient.Children.Remove(DataStckPatient.Children[0]);
                        break;
                    case (int)GridModelView.Hospital:
                        DataStckHospital.Children.Remove(DataStckHospital.Children[0]);
                        break;
                    case (int)GridModelView.Sickness:
                        DataStckSickness.Children.Remove(DataStckSickness.Children[0]);
                        break;
                    case (int)GridModelView.Doctor:
                        DataStckDoctor.Children.Remove(DataStckDoctor.Children[0]);
                        break;
                    case (int)GridModelView.Image:
                        DataStckImage.Children.Remove(DataStckImage.Children[0]);
                        break;
                    case (int)GridModelView.Country:
                        DataStckCountry.Children.Remove(DataStckCountry.Children[0]);
                        break;
                    case (int)GridModelView.City:
                        DataStckCity.Children.Remove(DataStckCity.Children[0]);
                        break;
                    case (int)GridModelView.Operation:
                        DataStckOperation.Children.Remove(DataStckOperation.Children[0]);
                        break;
                    case (int)GridModelView.UserPass:
                        DataStckUserPass.Children.Remove(DataStckUserPass.Children[0]);
                        break;
                }

                BtnAdd.Content = new PackIcon
                {
                    Kind = PackIconKind.Add,
                    Width = 32,
                    Height = 32,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }
            else if (!_isAdding) //start Adding
            {
                _isAdding ^= true;

                ClearEditor(false);
                BtnAdd.Content = new PackIcon
                {
                    Kind = PackIconKind.Close,
                    Width = 32,
                    Height = 32,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                #region Editor

                switch (ComboBoxTraverse.SelectedIndex)
                {
                    case (int)GridModelView.Section:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Section, new TblSection())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckSection.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForSection(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Patient:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Patient, new TblPatient())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckPatient.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForPatient(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Hospital:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Hospital, new TblHospital())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckHospital.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForHospital(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Sickness:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Sickness, new TblSickness())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckSickness.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForSickness(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Doctor:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Doctor, new TblDoctor())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckDoctor.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForDoctor(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Image:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Image, new TblImage())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckImage.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForImage(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Country:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Country, new TblCountry())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckCountry.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForCountry(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.City:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.City, new TblCity())
                        {
                            Style = (Style)FindResource("AddingMmDataBlock")
                        };
                        DataStckCity.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForCity(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.Operation:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Operation, new TblOperation());
                        DataStckOperation.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForOperation(_birthingMmDataBlock);
                        return;
                    case (int)GridModelView.UserPass:
                        _birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.UserPass, new TblUserPass());
                        DataStckUserPass.Children.Insert(0, _birthingMmDataBlock);
                        SetUpEditorForUserPass(_birthingMmDataBlock);
                        return;
                    default:
                        return;
                }

                #endregion
            }
        }

        public string CheckEditorInputs(GridModelView modelView)
        {
            switch (modelView)
            {
                case GridModelView.Section:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    return "☺";
                case GridModelView.Patient:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdDatePickerBirthDate.Text)) return "فیلد تاریخ تولد است";
                    if (!DateTime.TryParse(EdDatePickerBirthDate.Text, out _)) return " تاریخ تولد صحیح نمی باشد";
                    if (EdComboCountry.SelectedItem == null) return "کشور انتخاب نشده است";
                    if (EdComboCity.SelectedItem == null) return "شهر انتخاب نشده است";
                    if (string.IsNullOrEmpty(EdtxtPassNoOrIdentification.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdtxtHealthCode.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdtxtEmail.Text)) return "فیلد ایمیل خالی است";
                    if (string.IsNullOrEmpty(EdtxtTellNumber.Text)) return "فیلد شماره تلفن خالی است";
                    if (EdtxtTellNumber.Text.Length != 11) return "شماره تلفن صحیح نمی باشد";
                    if (string.IsNullOrEmpty(EdtxtAddress.Text)) return "آدرس وارد نشده است";
                    if (string.IsNullOrEmpty(EdtxtAmountPayed.Text)) return "مقدار پرداخت شده وارد نشده است";
                    if (!long.TryParse(EdtxtAmountPayed.Text, out _)) return "مقدار پرداخت شده صحیح نمی باشد";
                    if (string.IsNullOrEmpty(EdtxtCoShare.Text)) return "سهم شرکت وارد نشده است";
                    if (!long.TryParse(EdtxtCoShare.Text, out _)) return "سهم شرکت صحیح نمی باشد";
                    return "☺";
                case GridModelView.Hospital:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdtxtUserPassId.Text)) return "فیلد گذرواژه خالی است";
                    if (!long.TryParse(EdtxtUserPassId.Text, out _)) return "گذرواژه درست وارد نشده است";
                    if (string.IsNullOrEmpty(EdtxtPercentage.Text)) return "فیلد درصد خالی است";
                    if (!long.TryParse(EdtxtPercentage.Text, out _)) return "درصد درست وارد نشده است";
                    if (string.IsNullOrEmpty(EdtxtDescription.Text)) return "فیلد توضیحات خالی است";
                    if (string.IsNullOrEmpty(EdtxtLongtitude.Text)) return "فیلد طول جغرافیایی خالی است";
                    if (string.IsNullOrEmpty(EdtxtLatitude.Text)) return "فیلد عرض جغرافیایی خالی است";
                    return "☺";
                case GridModelView.Sickness:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (EdComboSection.SelectedItem == null) return "بخش انتخاب نشده است";
                    return "☺";
                case GridModelView.Doctor:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (EdComboSection.SelectedItem == null) return "بخش انتخاب نشده است";
                    return "☺";
                case GridModelView.Image:
                    //???
                    return "☺";
                case GridModelView.Country:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    return "☺";
                case GridModelView.City:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (EdComboCountry.SelectedItem == null) return "کشور انتخاب نشده است";
                    return "☺";
                case GridModelView.Operation:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdtxtPrice.Text)) return "فیلد قیمت خالی است";
                    if (!long.TryParse(EdtxtPrice.Text, out _)) return "قیمت درست وارد نشده است";
                    return "☺";
                case GridModelView.UserPass:
                    if (string.IsNullOrEmpty(EdtxtName.Text)) return "فیلد نام خالی است";
                    if (string.IsNullOrEmpty(EdtxtPassword.Text)) return "فیلد گذرواژه خالی است";
                    if (EdtxtPassword.Text.Length < 8) return "گذز واژه باید حداقل هشت کاراکتر باشد";
                    return "☺";
                default:
                    return "☺";
            }
        }

        public string CheckEditorInputsForEdit(GridModelView modelView)
        {
            switch (modelView)
            {
                case GridModelView.Section:
                    return "☺";
                case GridModelView.Patient:
                    if (!DateTime.TryParse(EdDatePickerBirthDate.Text, out _)) return " تاریخ تولد صحیح نمی باشد";
                    if (EdComboCountry.SelectedItem == null) return "کشور انتخاب نشده است";
                    if (EdComboCity.SelectedItem == null) return "شهر انتخاب نشده است";
                    if (EdtxtTellNumber.Text.Length != 11) return "شماره تلفن صحیح نمی باشد";
                    if (!long.TryParse(EdtxtAmountPayed.Text, out _)) return "مقدار پرداخت شده صحیح نمی باشد";
                    if (!long.TryParse(EdtxtCoShare.Text, out _)) return "سهم شرکت صحیح نمی باشد";
                    return "☺";
                case GridModelView.Hospital:
                    if (!long.TryParse(EdtxtUserPassId.Text, out _)) return "گذرواژه درست وارد نشده است";
                    if (!long.TryParse(EdtxtPercentage.Text, out _)) return "درصد درست وارد نشده است";
                    return "☺";
                case GridModelView.Sickness:
                    if (EdComboSection.SelectedItem == null) return "بخش انتخاب نشده است";
                    return "☺";
                case GridModelView.Doctor:
                    if (EdComboSection.SelectedItem == null) return "بخش انتخاب نشده است";
                    return "☺";
                case GridModelView.Image:
                    //???
                    return "☺";
                case GridModelView.Country:
                    return "☺";
                case GridModelView.City:
                    if (EdComboCountry.SelectedItem == null) return "کشور انتخاب نشده است";
                    return "☺";
                case GridModelView.Operation:
                    if (!long.TryParse(EdtxtPrice.Text, out _)) return "قیمت درست وارد نشده است";
                    return "☺";
                case GridModelView.UserPass:
                    if (EdtxtPassword.Text.Length < 8) return "گذز واژه باید حداقل هشت کاراکتر باشد";
                    return "☺";
                default:
                    return "☺";
            }
        }

        private async void EdBtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CallerWindow.ShowPreloader(CallerWindow.BrdPreLoader);
            EdBtnAdd.IsEnabled = false;
            switch (_birthingMmDataBlock.GridModelView)
            {
                case GridModelView.Section:
                    DataStckSection.Children.Remove(DataStckSection.Children[0]);
                    DataStckSection.Children.Insert(0, await AddSection(_birthingMmDataBlock));
                    break;
                case GridModelView.Patient:
                    DataStckPatient.Children.Remove(DataStckPatient.Children[0]);
                    DataStckPatient.Children.Insert(0, await AddPatient(_birthingMmDataBlock));
                    break;
                case GridModelView.Hospital:
                    DataStckHospital.Children.Remove(DataStckHospital.Children[0]);
                    DataStckHospital.Children.Insert(0, await AddHospital(_birthingMmDataBlock));
                    break;
                case GridModelView.Sickness:
                    DataStckSickness.Children.Remove(DataStckSickness.Children[0]);
                    DataStckSickness.Children.Insert(0, await AddSickness(_birthingMmDataBlock));
                    break;
                case GridModelView.Doctor:
                    DataStckDoctor.Children.Remove(DataStckDoctor.Children[0]);
                    DataStckDoctor.Children.Insert(0, await AddDoctor(_birthingMmDataBlock));
                    break;
                case GridModelView.Image:
                    DataStckImage.Children.Remove(DataStckImage.Children[0]);
                    DataStckImage.Children.Insert(0, await AddImage(_birthingMmDataBlock));
                    break;
                case GridModelView.Country:
                    DataStckCountry.Children.Remove(DataStckCountry.Children[0]);
                    DataStckCountry.Children.Insert(0, await AddCountry(_birthingMmDataBlock));
                    break;
                case GridModelView.City:
                    DataStckCity.Children.Remove(DataStckCity.Children[0]);
                    DataStckCity.Children.Insert(0, await AddCity(_birthingMmDataBlock));
                    break;
                case GridModelView.Operation:
                    DataStckOperation.Children.Remove(DataStckOperation.Children[0]);
                    DataStckOperation.Children.Insert(0, await AddOperation(_birthingMmDataBlock));
                    break;
                case GridModelView.UserPass:
                    DataStckUserPass.Children.Remove(DataStckUserPass.Children[0]);
                    DataStckUserPass.Children.Insert(0, await AddUserPass(_birthingMmDataBlock));
                    break;
            }

            ClearEditor();
            _isAdding = false;
            BtnAdd.Content = new PackIcon
            {
                Kind = PackIconKind.Add,
                Width = 32,
                Height = 32,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            //MetaMorphicDataBlock_MouseDown(BirthingMMDataBlock, new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
            //BirthingMMDataBlock.chkboxSelection.IsChecked ^= true;
            CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
            EdBtnAdd.IsEnabled = true;
        }

        private async Task<UIElement> AddUserPass(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblUserPass(EdtxtName.Text, EdtxtPassword.Text,
                EdCheckBoxIsHelthApple.IsChecked != null && (bool)EdCheckBoxIsHelthApple.IsChecked);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var userPassCore = new UserPassCore(token);
            await userPassCore.AddUserPass((TblUserPass)birthingMmDataBlock.DataModel);
            var reflectedUserPass =
                await userPassCore.SelectUserPassByUsername(((TblUserPass)birthingMmDataBlock.DataModel).Username);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.UserPass,
                new TblUserPass(reflectedUserPass.Id, reflectedUserPass.Username, reflectedUserPass.Password,
                    reflectedUserPass.IsHelthApple));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddOperation(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblOperation(EdtxtName.Text, long.Parse(EdtxtPrice.Text));

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var operationCore = new OperationCore(token);
            await operationCore.AddOperation((TblOperation)birthingMmDataBlock.DataModel);
            var reflectedOperation =
                await operationCore.SelectOperationByOperationName(((TblOperation)birthingMmDataBlock.DataModel)
                    .OperationName);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Operation,
                new TblOperation(reflectedOperation.Id, reflectedOperation.OperationName,
                    reflectedOperation.OperationPrice));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddCity(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblCity(
                EdtxtName.Text,
                ((TblCountry)((ComboBoxItem)EdComboCountry.SelectedItem).Tag).Id);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var cityCore = new CityCore(token);
            await cityCore.AddCity((TblCity)birthingMmDataBlock.DataModel);
            var reflectedCity = await cityCore.SelectCityByName(((TblCity)birthingMmDataBlock.DataModel).Name);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.City, new TblCity(reflectedCity.Id,
                reflectedCity.Name
                , reflectedCity.CountryId));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddCountry(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblCountry(EdtxtName.Text);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var countryCore = new CountryCore(token);
            var reflectedCountry = await countryCore.AddCountry((TblCountry)birthingMmDataBlock.DataModel);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Country,
                new TblCountry(reflectedCountry.Id, reflectedCountry.Name));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddImage(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblImage(EdtxtName.Text, EdComboImageStatus.SelectedIndex);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var imageCore = new ImageCore(token);
            await imageCore.AddImage((TblImage)birthingMmDataBlock.DataModel);
            var reflectedImage = await imageCore.SelectImageByImage(((TblImage)birthingMmDataBlock.DataModel).Image);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Image, new TblImage(reflectedImage.Id,
                reflectedImage.Image, reflectedImage.Status));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddDoctor(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblDoctor(
                EdtxtName.Text,
                ((TblSection)((ComboBoxItem)EdComboSection.SelectedItem).Tag).Id
                , EdCheckBoxIsActive.IsChecked != null && (bool)EdCheckBoxIsActive.IsChecked);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var doctorCore = new DoctorCore(token);
            await doctorCore.AddDoctor((TblDoctor)birthingMmDataBlock.DataModel);
            var reflectedDoctor = await doctorCore.SelectDoctorByName(((TblDoctor)birthingMmDataBlock.DataModel).Name);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Doctor,
                new TblDoctor(reflectedDoctor.Id, reflectedDoctor.Name, reflectedDoctor.SectionId,
                    EdCheckBoxIsActive.IsChecked != null && (bool)EdCheckBoxIsActive.IsChecked));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddSickness(MetaMorphicDataBlock birthingMmDataBlock)
        {

            birthingMmDataBlock.DataModel = new TblSickness(
                EdtxtName.Text,
                ((TblSection)((ComboBoxItem)EdComboSection.SelectedItem).Tag).Id);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var sicknessCore = new SicknessCore(token);
            await sicknessCore.AddSickness((TblSickness)birthingMmDataBlock.DataModel);
            var reflectedSickness =
                await sicknessCore.SelectSicknessByName(((TblSickness)birthingMmDataBlock.DataModel).Name);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Sickness,
                new TblSickness(reflectedSickness.Id, reflectedSickness.Name, reflectedSickness.SectionId));
            return birthingMmDataBlock;
        }

        private async Task<MetaMorphicDataBlock> AddHospital(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblHospital(EdtxtName.Text, int.Parse(EdtxtUserPassId.Text),
                int.Parse(EdtxtPercentage.Text), EdtxtDescription.Text, EdtxtLongtitude.Text, EdtxtLatitude.Text);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var hospitalCore = new HospitalCore(token);
            await hospitalCore.AddHospital((TblHospital)birthingMmDataBlock.DataModel);
            var reflectedHospital =
                await hospitalCore.SelectHospitalByName(((TblHospital)birthingMmDataBlock.DataModel).Name);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Hospital,
                new TblHospital(
                    reflectedHospital.Id, reflectedHospital.Name, reflectedHospital.UserPassId
                    , reflectedHospital.Percentage, reflectedHospital.Description, reflectedHospital.Longitude
                    , reflectedHospital.Latitude));
            return birthingMmDataBlock;
        }

        //
        private async Task<MetaMorphicDataBlock> AddPatient(MetaMorphicDataBlock birthingMmDataBlock)
        {
            birthingMmDataBlock.DataModel = new TblPatient(EdtxtName.Text, EdRadioMale.IsChecked != null && (bool)EdRadioMale.IsChecked,
                EdDatePickerBirthDate.Text,
                ((TblCountry)((ComboBoxItem)EdComboCountry.SelectedItem).Tag).Id,
                ((TblCity)((ComboBoxItem)EdComboCity.SelectedItem).Tag).Id,
                EdtxtPassNoOrIdentification.Text,
                EdtxtHealthCode.Text,
                EdtxtEmail.Text,
                EdtxtTellNumber.Text,
                EdtxtAddress.Text,
                long.Parse(EdtxtAmountPayed.Text),
                long.Parse(EdtxtCoShare.Text),
                EdDatePickerReleaseDate.Text,
                int.Parse(EdtxtUserPassId.Text),
                EdComboStatus.SelectedIndex,
                EdDatePickerTimeRegistered.Text,
                EdtxtParentalName.Text,
                EdtxtJob.Text,
                EdtxtInsurance.Text,
                EdtxtHelpName.Text,
                EdtxtHelpJob.Text,
                EdtxtHelpPassNo.Text,
                EdtxtHelpTelNo.Text);

            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            var patientCore = new PatientCore(token);
            await patientCore.AddPatient((TblPatient)birthingMmDataBlock.DataModel);
            var reflectedPatients = await
                patientCore.SelectPatientByPassNoOrIdentification(int.Parse(((TblPatient)birthingMmDataBlock.DataModel)
                    .PassNoOrIdentification));
            var reflectedPatient = reflectedPatients[0];
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Patient, new TblPatient(reflectedPatient.Id,
                reflectedPatient.Name, reflectedPatient.IsMan, reflectedPatient.BirthDate, reflectedPatient.CountryId
                , reflectedPatient.CityId, reflectedPatient.PassNoOrIdentification, reflectedPatient.HelthCode,
                reflectedPatient.Email, reflectedPatient.TellNo, reflectedPatient.Address, reflectedPatient.Payed,
                reflectedPatient.CoShare, reflectedPatient.DateReleased, reflectedPatient.UserPassId,
                reflectedPatient.Status,
                reflectedPatient.TimeRegistered, reflectedPatient.ParentalName, reflectedPatient.Job,
                reflectedPatient.Insurance, reflectedPatient.HelpName, reflectedPatient.HelpJob,
                reflectedPatient.HelpPassNo, reflectedPatient.HelpTelNo));
            return birthingMmDataBlock;
        }

        //
        private async Task<MetaMorphicDataBlock> AddSection(MetaMorphicDataBlock birthingMmDataBlock)
        {
            SecurityCore securityCore = new SecurityCore();
            string token = await securityCore.GenerateToken(Username, Password);
            birthingMmDataBlock.DataModel = new TblSection(EdtxtName.Text);
            var sectionCore = new SectionCore(token);
            await sectionCore.AddSection((TblSection)birthingMmDataBlock.DataModel);
            var reflectedSection =
                await sectionCore.SelectSectionBySectionName(((TblSection)birthingMmDataBlock.DataModel).SectionName);
            birthingMmDataBlock = new MetaMorphicDataBlock(GridModelView.Section,
                new TblSection(reflectedSection.Id, reflectedSection.SectionName));
            return birthingMmDataBlock;
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BtnRefresh.IsEnabled = false;
            CallerWindow.ShowPreloader(CallerWindow.BrdPreLoader);

            try
            {

                switch (ComboBoxTraverse.SelectedIndex)
                {
                    case (int)GridModelView.Section:
                        ArrSections.Clear();
                        DataStckSection.Children.Clear();
                        await FetchSection();
                        break;
                    case (int)GridModelView.Patient:
                        ArrPatient.Clear();
                        DataStckPatient.Children.Clear();
                        await FetchPatient();
                        break;
                    case (int)GridModelView.Hospital:
                        ArrHospital.Clear();
                        DataStckHospital.Children.Clear();
                        await FetchHospital();
                        break;
                    case (int)GridModelView.Sickness:
                        ArrSickness.Clear();
                        DataStckSickness.Children.Clear();
                        await FetchSickness();
                        break;
                    case (int)GridModelView.Doctor:
                        ArrDoctor.Clear();
                        DataStckDoctor.Children.Clear();
                        await FetchDoctor();
                        break;
                    case (int)GridModelView.Image:
                        ArrImage.Clear();
                        DataStckImage.Children.Clear();
                        await FetchImage();
                        break;
                    case (int)GridModelView.Country:
                        ArrCountry.Clear();
                        DataStckCountry.Children.Clear();
                        await FetchCountry();
                        break;
                    case (int)GridModelView.City:
                        ArrCity.Clear();
                        DataStckCity.Children.Clear();
                        await FetchCity();
                        break;
                    case (int)GridModelView.Operation:
                        ArrOperations.Clear();
                        DataStckOperation.Children.Clear();
                        await FetchOperation();
                        break;
                    case (int)GridModelView.UserPass:
                        ArrUserPass.Clear();
                        DataStckUserPass.Children.Clear();
                        await FetchUserPass();
                        break;
                }

            }
            catch (Exception exp)
            {
                BtnRefresh.IsEnabled = true;
                new BlurOutMessageBox().Show(exp.Message);
                CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
                return;
            }


            BtnRefresh.IsEnabled = true;
            CallerWindow.HidePreloader(CallerWindow.BrdPreLoader);
        }

        private void CheckBoxSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;
            if (check.Name == "ChkboxSelection")
                switch (ComboBoxTraverse.SelectedIndex)
                {
                    case (int)GridModelView.Section:
                        foreach (var ctrl in DataStckSection.Children)
                        {
                            var blockSection = (MetaMorphicDataBlock)ctrl;
                            blockSection.ChkboxSelection.IsChecked ^= true;
                            if (blockSection.Visibility == Visibility.Collapsed) continue;

                            MetaMorphicDataBlock_MouseDown(blockSection, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Patient:
                        foreach (var ctrl in DataStckPatient.Children)
                        {
                            var blockPatient = (MetaMorphicDataBlock)ctrl;
                            blockPatient.ChkboxSelection.IsChecked ^= true;
                            if (blockPatient.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockPatient, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Hospital:
                        foreach (var ctrl in DataStckHospital.Children)
                        {
                            var blockHospital = (MetaMorphicDataBlock)ctrl;
                            blockHospital.ChkboxSelection.IsChecked ^= true;
                            if (blockHospital.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockHospital, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Sickness:
                        foreach (var ctrl in DataStckSickness.Children)
                        {
                            var blockSickness = (MetaMorphicDataBlock)ctrl;
                            blockSickness.ChkboxSelection.IsChecked ^= true;
                            if (blockSickness.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockSickness, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Doctor:
                        foreach (var ctrl in DataStckDoctor.Children)
                        {
                            var blockDoctor = (MetaMorphicDataBlock)ctrl;
                            blockDoctor.ChkboxSelection.IsChecked ^= true;
                            if (blockDoctor.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockDoctor, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Image:
                        foreach (var ctrl in DataStckImage.Children)
                        {
                            var blockImage = (MetaMorphicDataBlock)ctrl;
                            blockImage.ChkboxSelection.IsChecked ^= true;
                            if (blockImage.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockImage, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Country:
                        foreach (var ctrl in DataStckCountry.Children)
                        {
                            var blockCountry = (MetaMorphicDataBlock)ctrl;
                            blockCountry.ChkboxSelection.IsChecked ^= true;
                            if (blockCountry.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockCountry, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.City:
                        foreach (var ctrl in DataStckCity.Children)
                        {
                            var blockCity = (MetaMorphicDataBlock)ctrl;
                            blockCity.ChkboxSelection.IsChecked ^= true;
                            if (blockCity.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockCity, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.Operation:
                        foreach (var ctrl in DataStckOperation.Children)
                        {
                            var blockOperation = (MetaMorphicDataBlock)ctrl;
                            blockOperation.ChkboxSelection.IsChecked ^= true;
                            if (blockOperation.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockOperation, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                    case (int)GridModelView.UserPass:
                        foreach (var ctrl in DataStckUserPass.Children)
                        {
                            var blockUserPass = (MetaMorphicDataBlock)ctrl;
                            blockUserPass.ChkboxSelection.IsChecked ^= true;
                            if (blockUserPass.Visibility == Visibility.Collapsed) continue;
                            MetaMorphicDataBlock_MouseDown(blockUserPass, new MouseButtonEventArgs(Mouse.PrimaryDevice,
                                0, MouseButton.Left));
                        }

                        break;
                }
        }

        private void SearchField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // See if the sender was a button
            var field = (SearchField)sender;
        }

        private void MenuSelect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var a = sender;
        }

        private void MenuDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var a = sender;
        }
    }
}