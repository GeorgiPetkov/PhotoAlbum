namespace Presentation.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    abstract class ValueConverter<From, To> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert((From)value);

        protected virtual To Convert(From value) => throw new NotSupportedException();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack((To)value);

        protected virtual From ConvertBack(To value) => throw new NotSupportedException();
    }
}
