using DashBoard.ViewModels;
using System;

namespace DashBoard
{
    public class SearchDateTimeViewModel : BaseViewModel
    {
        public string FieldName { get; set; } = "نوع داده";
        public DateTime FieldDateTime { get; set; } = DateTime.Now;

        public SearchDateTimeViewModel()
        {

        }
        public SearchDateTimeViewModel(string fieldName, DateTime fieldDateTime)
        {
            FieldName = fieldName;
            FieldDateTime = fieldDateTime;
        }
    }
}
