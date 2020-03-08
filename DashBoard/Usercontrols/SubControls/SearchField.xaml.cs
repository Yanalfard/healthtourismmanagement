using System.Windows.Controls;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for SearchField.xaml
    /// </summary>
    public partial class SearchField : UserControl
    {
        SearchFieldViewModel _viewModel;

        public SearchField()
        {
            InitializeComponent();
        }

        public int PropertyIndex { get; set; }

        public SearchField(string fieldName, string fieldText)
        {
            _viewModel = new SearchFieldViewModel(fieldName, fieldText);
            this.Name = fieldName;
            this.DataContext = _viewModel;
            InitializeComponent();

            TxtInput.DataContext = _viewModel;
        }
    }
}
