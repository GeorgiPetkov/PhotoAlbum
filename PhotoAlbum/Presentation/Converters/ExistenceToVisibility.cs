namespace Presentation.Converters
{
    using System.Windows;

    class ExistenceToVisibility : ValueConverter<object, Visibility>
    {
        protected override Visibility Convert(object o) => o == null ? Visibility.Hidden : Visibility.Visible;
    }
}
