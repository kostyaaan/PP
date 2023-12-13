using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Linq;
using System.Globalization;

namespace WpfApp1
{
    //Конвертер для класса XElement. Задача этого конвертера - извлечение 
    // вложенных элементов типа XElement, инкапсулирующих
    //в себе разделы Category.

    public sealed class XmlConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is XElement)
            {
                XElement x = (XElement)value;
                return x.Elements("Category");
            }
            else
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
