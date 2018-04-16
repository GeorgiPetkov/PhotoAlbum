namespace Presentation.Windows
{
    using Logic;
    using Prism.Commands;
    using System;
    using System.Windows.Input;
    using static Logic.Services;

    partial class AddCategoryWindow : Window
    {
        internal event EventHandler<CategoryAddedEventArgs> CategoryAdded;

        public string CategoryName { private get; set; }
        public ICommand AddCategoryCommand { get; }

        internal AddCategoryWindow(Guid userId, Guid? parentId)
        {
            AddCategoryCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(CategoryName))
                {
                    ErrorMessage = "The name is required.";
                    return;
                }

                Category category = CategoriesManager.Create(CategoryName.Trim(), userId, parentId);
                CategoryAdded?.Invoke(this, new CategoryAddedEventArgs { Category = category });
                Close();
            });
        }
    }

    class CategoryAddedEventArgs
    {
        internal Category Category { get; set; }
    }
}
