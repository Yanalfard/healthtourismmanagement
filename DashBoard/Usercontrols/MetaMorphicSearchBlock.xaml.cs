using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace DashBoard
{
    /// <summary>
    /// A single row that shows the properties of the DataModel
    /// that is inputted on the classes construction.
    /// </summary>
    public partial class MetaMorphicSearchBlock : UserControl
    {
        #region Properties
        /// <summary>
        /// Current data Type
        /// </summary>
        public GridModelView GridModelView { get; }
        /// <summary>
        /// Current data model that is passed in
        /// </summary>
        public object DataModelMask { get; }
        /// <summary>
        /// An index sorted list of the properties available in this dataModel
        /// </summary>
        public PropertyInfo[] DataProperties { get; }
        #endregion
        [Obsolete]
        public MetaMorphicSearchBlock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public MetaMorphicSearchBlock(GridModelView modelView, object dataModel)
        {
            InitializeComponent();
            GridModelView = modelView;
            DataModelMask = dataModel;
            DataProperties = dataModel.GetType().GetProperties();

            GenerateFields(dataModel, DataProperties);
        }
        /// <summary>
        /// Generate the correct controls for the fields in the <see cref="DataModelMask"/> that is passed in
        /// </summary>
        private void GenerateFields(object dataModel, PropertyInfo[] properties)
        {
            int propertyIndexCounter = 0;
            foreach (var pro in properties)
            {
                #region old
                //Int
                if (PropertyInfo.Equals(pro.PropertyType, typeof(int)))
                {
                    SearchField searchField = new SearchField(pro.Name, "") { Width = 180, MaxWidth = 180, PropertyIndex = propertyIndexCounter};
                    searchField.BtnSearch.Tag = propertyIndexCounter;
                    MainStackPanel.Children.Add(searchField);
                }
                //String
                else if (PropertyInfo.Equals(pro.PropertyType, typeof(string)))
                {
                    SearchField searchField = new SearchField(pro.Name, "") { Width = 180, MaxWidth = 180, PropertyIndex = propertyIndexCounter };
                    searchField.BtnSearch.Tag = propertyIndexCounter;
                    MainStackPanel.Children.Add(searchField);

                }
                //Bool
                else if (PropertyInfo.Equals(pro.PropertyType, typeof(bool)))
                {
                    SearchCheckBox checkBox = new SearchCheckBox(pro.Name, false) { Width=180, MaxWidth = 180, PropertyIndex = propertyIndexCounter };
                    checkBox.BtnSearch.Tag = propertyIndexCounter;
                    MainStackPanel.Children.Add(checkBox);

                }
                //DateTime
                else if(PropertyInfo.Equals(pro.PropertyType, typeof(DateTime)))
                {
                    SearchDateTime dateTime = new SearchDateTime() { Width = 180, MaxWidth = 180 , PropertyIndex = propertyIndexCounter};
                    dateTime.BtnSearch.Tag = propertyIndexCounter;
                    MainStackPanel.Children.Add(dateTime);
                }
                #endregion

                propertyIndexCounter++;
            }
        }


        /// <summary>
        /// Occures when a checkbox residing in a <see cref="MetaMorphicDataBlock"/> is Checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        #region MyRegion
        /// <summary>
        /// Occures when a Textbox residing in a <see cref="MetaMorphicDataBlock"/> has Gotten Focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Occures when a Textbox residing in a <see cref="MetaMorphicDataBlock"/> has Lost Focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //Check if the new value and the last value were the same
        }

        #endregion

        private void ChkboxSelection_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
