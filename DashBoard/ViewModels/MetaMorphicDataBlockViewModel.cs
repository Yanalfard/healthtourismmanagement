using System.Reflection;

namespace DashBoard.ViewModels
{
    /// <summary>
    /// View model for a MetaMorphicDataBlock,
    /// </summary>
    /// <typeparam name="T">Type of the DataModel this DataBlock depicts</typeparam>
    public class MetaMorphicDataBlockViewModel<T> : BaseViewModel
    {
        static public T DataModel { get; set; }

        public PropertyInfo[] ModelProperties { get; set; } = DataModel.GetType().GetProperties();

        public MetaMorphicDataBlockViewModel()
        {
            var a = DataModel.GetType().GetProperties();

        }

        public void GenerateProperties()
        {
            foreach (var property in ModelProperties)
            {
                var type = property.GetType();
                var name = property.Name;
            }
        }

    }
}
