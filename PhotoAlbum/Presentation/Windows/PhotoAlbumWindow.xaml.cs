namespace Presentation.Windows
{
    using Presentation.Pages;
    using static System.Windows.SizeToContent;

    partial class PhotoAlbumWindow : Window
    {
        public PhotoAlbumWindow()
        {
            SizeToContent = Manual;
            Content = new LoginRegisterPage();
        }
    }
}
