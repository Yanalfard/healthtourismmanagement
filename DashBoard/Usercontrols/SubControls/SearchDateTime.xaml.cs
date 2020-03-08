using System;
using System.Windows.Controls;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for SearchField.xaml
    /// </summary>
    public partial class SearchDateTime : UserControl
    {
        SearchDateTimeViewModel _viewModel;

        public SearchDateTime()
        {
            InitializeComponent();
        }
        public int PropertyIndex { get; set; }

        public SearchDateTime(string fieldName, DateTime fieldDateTime)
        {
            _viewModel = new SearchDateTimeViewModel(fieldName, fieldDateTime);
            this.Name = fieldName;
            this.DataContext = _viewModel;
            InitializeComponent();

            TxtInput.DataContext = _viewModel;
        }
    }
}
