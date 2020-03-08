using DashBoard.ViewModels;

namespace DashBoard
{
    public class SearchCheckBoxViewModel : BaseViewModel
    {
        public string FieldName { get; set; } = "نوع داده";
        public bool FieldBit { get; set; } = false;

        public SearchCheckBoxViewModel()
        {

        }
        public SearchCheckBoxViewModel(string fieldName, bool fieldBit)
        {
            FieldName = fieldName;
            FieldBit = fieldBit;
        }
    }
}
