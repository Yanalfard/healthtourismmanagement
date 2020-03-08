using System.Windows.Controls;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for SearchField.xaml
    /// </summary>
    public partial class SearchCheckBox : UserControl
    {
        SearchCheckBoxViewModel _viewModel;

        public SearchCheckBox()
        {
            InitializeComponent();
        }
        public int PropertyIndex { get; set; }

        public SearchCheckBox(string fieldName, bool fieldBit)
        {
            _viewModel = new SearchCheckBoxViewModel(fieldName, fieldBit);
            this.Name = fieldName;
            this.DataContext = _viewModel;
            InitializeComponent();

            ChckSearch.DataContext = _viewModel;
        }
    }
}
