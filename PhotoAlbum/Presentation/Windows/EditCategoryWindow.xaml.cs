namespace Presentation.Windows
{
    using Logic;
    using Prism.Commands;
    using System;
    using System.Windows.Input;
    using static Logic.Services;

    partial class EditCategoryWindow : Window
    {
        internal event EventHandler<CategoryEditedEventArgs> CategoryEdited;

        public string CategoryName { get; set; }
        public ICommand EditCategoryCommand { get; }

        internal EditCategoryWindow(Category category)
        {
            CategoryName = category.Name;
            EditCategoryCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(CategoryName))
                {
                    ErrorMessage = "The name is required.";
                    return;
                }

                CategoryEdited?.Invoke(this, new CategoryEditedEventArgs
                {
                    Category = CategoriesManager.Rename(category.Id, CategoryName.Trim())
                });
                Close();
            });
        }
    }

    class CategoryEditedEventArgs
    {
        internal Category Category { get; set; }
    }
}
