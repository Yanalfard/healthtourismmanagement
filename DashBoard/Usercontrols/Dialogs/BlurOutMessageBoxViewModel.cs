using PropertyChanged;
using System.ComponentModel;
using System.Windows;

namespace DashBoard
{
    [AddINotifyPropertyChangedInterface]
    public class BlurOutMessageBoxViewModel : INotifyPropertyChanged
    {
        public string MessageTitle { get; set; } = "";
        public string Message { get; set; } = "پیام";
        public string AcceptText { get; set; } = "بله";
        public string RejectText { get; set; } = "خیر";
        public string CancelText { get; set; } = "لغو";
        public MessageBoxResult Result { get; set; } = MessageBoxResult.Cancel;

        //public string MessageTitle { get; set; } 
        //public string Message { get; set; } 
        //public string AcceptText { get; set; }
        //public string RejectText { get; set; }
        //public string CancelText { get; set; }
        //public Window CallerWindow { get; set; }
        //public MessageBoxResult Result { get; set; }

        public BlurOutMessageBoxViewModel()
        {

        }

        public BlurOutMessageBoxViewModel(string message)
        {
            Message = message;
        }

        public BlurOutMessageBoxViewModel(string message, string acceptText)
        {
            Message = message;
            AcceptText = acceptText;
        }

        public BlurOutMessageBoxViewModel(string title, string message, string acceptText)
        {
            MessageTitle = title;
            Message = message;
            AcceptText = acceptText;
        }

        public BlurOutMessageBoxViewModel(string title, string message, string acceptText, string rejectText)
        {
            MessageTitle = title;
            Message = message;
            AcceptText = acceptText;
            RejectText = rejectText;
        }

        public BlurOutMessageBoxViewModel(string title, string message, string acceptText, string rejectText, string cancelText)
        {
            MessageTitle = title;
            Message = message;
            AcceptText = acceptText;
            RejectText = rejectText;
            CancelText = cancelText;
        }

        /// <summary>
        /// The event that is fired when A property in this class is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {
            var a = (BlurOutMessageBoxViewModel)sender;
            
        };
    }
}
