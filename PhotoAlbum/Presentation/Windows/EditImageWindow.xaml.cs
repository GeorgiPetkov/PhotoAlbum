namespace Presentation.Windows
{
    using Logic;
    using Prism.Commands;
    using System;
    using System.Windows.Input;
    using static Logic.Services;

    partial class EditImageWindow : Window
    {
        internal event EventHandler<ImageEditedEventArgs> ImageEdited;

        public string ImageName { get; set; }
        public string Description { get; set; }
        public ICommand EditImageCommand { get; }

        internal EditImageWindow(Image image)
        {
            ImageName = image.Name;
            Description = image.Description;
            EditImageCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(ImageName))
                {
                    ErrorMessage = "The name is required.";
                    return;
                }

                string description = string.IsNullOrWhiteSpace(Description) ? null : Description;
                ImageEdited?.Invoke(this, new ImageEditedEventArgs
                {
                    Image = ImagesManager.Edit(image.Id, ImageName.Trim(), description)
                });
                Close();
            });
        }
    }

    class ImageEditedEventArgs
    {
        internal Image Image { get; set; }
    }
}
