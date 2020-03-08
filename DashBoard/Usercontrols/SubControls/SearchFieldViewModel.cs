using DashBoard.ViewModels;

namespace DashBoard
{
    public class SearchFieldViewModel : BaseViewModel
    {
        public string FieldName { get; set; } = "نوع داده";
        public string FieldText { get; set; } = "داده";

        public SearchFieldViewModel()
        {

        }
        public SearchFieldViewModel(string fieldName, string fieldText)
        {
            FieldName = fieldName;
            FieldText = fieldText;
        }
    }
}
