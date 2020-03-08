using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;

namespace DashBoard
{
    /// <summary>
    /// A single row that shows the properties of the DataModel
    /// that is inputted on the classes construction.
    /// </summary>
    public partial class MetaMorphicDataBlock : UserControl
    {
        #region Properties
        /// <summary>
        /// Current data Type
        /// </summary>
        public GridModelView GridModelView { get; }
        /// <summary>
        /// Current data model that is passed in
        /// </summary>
        public object DataModel { get; set; }
        /// <summary>
        /// An index sorted list of the properties available in this dataModel
        /// </summary>
        public PropertyInfo[] DataProperties { get; }
        #endregion
        [Obsolete]
        public MetaMorphicDataBlock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// default constructor
        /// </summary>
        public MetaMorphicDataBlock(GridModelView modelView, object dataModel)
        {
            InitializeComponent();
            GridModelView = modelView;
            DataModel = dataModel;
            DataProperties = dataModel.GetType().GetProperties();

            GenerateFields(dataModel, DataProperties);
        }
        /// <summary>
        /// Generate the correct controls for the fields in the <see cref="DataModel"/> that is passed in
        /// </summary>
        private void GenerateFields(object dataModel, PropertyInfo[] properties)
        {
            int propertyIndexCounter = 0;
            foreach (var pro in properties)
            {
                //Int
                if (PropertyInfo.Equals(pro.PropertyType, typeof(int)))
                {
                    TextBlock textBlock = new TextBlock()
                    {
                        Tag = propertyIndexCounter,
                        Name = pro.Name,
                        Text = pro.GetValue(DataModel).ToString() ?? "",
                        Width = 180,
                        MaxWidth = 180,
                        Style = (Style)FindResource("GenTxt"),
                        IsHitTestVisible = false,
                };
                    //It's an ID
                    if (pro.Name.EndsWith("Id") || pro.Name == "id")
                    {
                        textBlock.Width = 180;
                    }
                    MainStackPanel.Children.Add(textBlock);
                }
                //String
                else if (PropertyInfo.Equals(pro.PropertyType, typeof(string)))
                {
                    TextBlock textBox = new TextBlock()
                    {
                        //Width = MainStackPanel.Width / properties.Length,
                        Tag = propertyIndexCounter,
                        Name = pro.Name,
                        Width = 180,
                        MaxWidth = 180,
                        Text = (pro.GetValue(DataModel) ?? "").ToString(),
                        Style = (Style)FindResource("GenTxt"),
                        IsHitTestVisible = false,
                    };
                    textBox.GotFocus += PropertyTextBox_GotFocus;
                    textBox.LostFocus += PropertyTextBox_LostFocus;
                    MainStackPanel.Children.Add(textBox);
                }
                //Bool
                else if (PropertyInfo.Equals(pro.PropertyType, typeof(bool)))
                {
                    CheckBox checkBox = new CheckBox()
                    {
                        //Width = MainStackPanel.Width / properties.Length,
                        Tag = propertyIndexCounter,
                        Name = pro.Name,
                        IsChecked = ((bool)pro.GetValue(DataModel)) ? true : false,
                        Style = (Style)FindResource("GenChkBox"),
                        Width = 180,
                        MaxWidth = 180,
                        IsHitTestVisible = false
                    };
                    checkBox.Checked += PropertyCheckBox_Checked;
                    MainStackPanel.Children.Add(checkBox);


                }
                //DateTime
                else if (PropertyInfo.Equals(pro.PropertyType, typeof(DateTime)))
                {
                    TextBlock dateTime = new TextBlock()
                    {
                        Tag = propertyIndexCounter,
                        Name = pro.Name,
                        Text = pro.GetValue(DataModel).ToString(),
                        Style = (Style)FindResource("GenTxt"),
                        IsHitTestVisible = false,
                        Width = 180,
                        MaxWidth = 180,

                    };
                    MainStackPanel.Children.Add(dateTime);
                }
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

        private void MMDataBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChkboxSelection.IsChecked ^= true;
        }

        private void ChkboxSelection_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
