namespace Presentation.Windows
{
    using Logic;
    using Microsoft.Win32;
    using Prism.Commands;
    using System;
    using System.Windows.Input;
    using static Logic.Services;
    using static System.IO.File;

    partial class AddImageWindow : Window
    {
        internal event EventHandler<ImageAddedEventArgs> ImageAdded;

        public string ImageName { private get; set; }
        public string Description { private get; set; }
        string filePath;
        public string FilePath
        {
            get => filePath;
            private set
            {
                filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        public ICommand OpenImageBrowserCommand { get; }
        public ICommand AddImageCommand { get; }

        internal AddImageWindow(Guid categoryId)
        {
            OpenImageBrowserCommand = new DelegateCommand(() =>
            {
                OpenFileDialog selectImageDialog = new OpenFileDialog
                {
                    Title = "Select Image",
                    Filter = "Image (.png)|*.png",
                    CheckFileExists = true,
                    ValidateNames = true
                };
                selectImageDialog.FileOk += (sender, args) =>
                {
                    FilePath = selectImageDialog.FileName;
                };

                selectImageDialog.ShowDialog();
            });
            AddImageCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(ImageName) || string.IsNullOrWhiteSpace(FilePath))
                {
                    ErrorMessage = "The name and path are required.";
                    return;
                }

                string description = string.IsNullOrWhiteSpace(Description) ? null : Description;
                Image image = ImagesManager.Create(
                    ImageName.Trim(), description, ReadAllBytes(FilePath.Trim()), categoryId);
                ImageAdded?.Invoke(this, new ImageAddedEventArgs { Image = image });
                Close();
            });
        }
    }

    class ImageAddedEventArgs
    {
        internal Image Image { get; set; }
    }
}
