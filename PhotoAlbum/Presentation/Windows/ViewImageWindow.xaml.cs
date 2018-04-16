namespace Presentation.Windows
{
    using Logic;

    partial class ViewImageWindow : Window
    {
        public Image Image { get; }

        internal ViewImageWindow(Image image)
        {
            Image = image;
        }
    }
}
