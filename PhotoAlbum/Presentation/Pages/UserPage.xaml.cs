namespace Presentation.Pages
{
    using Extensions;
    using Logic;
    using Presentation.Windows;
    using Prism.Commands;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using static Logic.Services;

    partial class UserPage : Page
    {
        User user;
        public User User
        {
            get => user;
            private set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public ObservableCollection<CategoryWithContent> Breadcrumb { get; }
        public Category Category => Breadcrumb.LastOrDefault();
        public bool IsInCategory => Breadcrumb.Any();
        public ObservableCollection<InventoryItem> Inventory { get; }
        InventoryItem selectedItem;
        public InventoryItem SelectedItem
        {
            private get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        bool HasSelectedItem => SelectedItem != null;
        public ICommand OpenBreadcrumbCategoryCommand { get; }
        public ICommand ViewItemCommand { get; }
        public ICommand OpenEditProfileWindowCommand { get; }
        public ICommand OpenAddCategoryWindowCommand { get; }
        public ICommand OpenAddImageWindowCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        internal UserPage(User user)
        {
            User = user;
            Breadcrumb = new ObservableCollection<CategoryWithContent>();
            Breadcrumb.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(Breadcrumb));
            Inventory = new ObservableCollection<InventoryItem>(
                CategoriesManager.GetRootCategoriesByUser(User.Id).Select(c => new CategoryItem(c)));
            SelectedItem = null;
            OpenBreadcrumbCategoryCommand = new DelegateCommand<CategoryWithContent>(OpenBreadcrumbCategory);
            ViewItemCommand = new DelegateCommand<InventoryItem>(ViewItem);
            OpenEditProfileWindowCommand = new DelegateCommand(() => OpenEditProfileWindow(User));
            OpenAddCategoryWindowCommand = new DelegateCommand(OpenAddCategoryWindow);
            OpenAddImageWindowCommand = new DelegateCommand(OpenAddImageWindow, () => IsInCategory)
                .ObservesProperty(() => Breadcrumb);
            EditItemCommand = new DelegateCommand(OpenEditItemWindow, () => HasSelectedItem)
                .ObservesProperty(() => SelectedItem);
            DeleteItemCommand = new DelegateCommand(OpenDeleteItemWindow, () => HasSelectedItem)
                .ObservesProperty(() => SelectedItem);
        }

        void OpenBreadcrumbCategory(CategoryWithContent category)
        {
            Breadcrumb.Skip(Breadcrumb.IndexOf(category)).ToList().ForEach(c => Breadcrumb.Remove(c));
            OpenCategory(category.Id);
        }

        void ViewItem(InventoryItem item)
        {
            switch (item)
            {
                case CategoryItem categoryItem:
                    OpenCategory(categoryItem.Category.Id);
                    break;
                case ImageItem imageItem:
                    OpenViewImageWindow(imageItem.Image);
                    break;
                default: throw new ArgumentException("Unsupported item type provided.");
            }
        }

        void OpenCategory(Guid categoryId)
        {
            CategoryWithContent category = CategoriesManager.Get(categoryId);
            Breadcrumb.Add(category);
            UpdateInventory(category);
        }

        void OpenViewImageWindow(Image image) => new ViewImageWindow(image).ShowDialog();

        void OpenEditProfileWindow(User user)
        {
            EditProfileWindow window = new EditProfileWindow(user);
            window.ProfileEdited += (sender, args) => User = args.User;
            window.ShowDialog();
        }

        void OpenAddCategoryWindow()
        {
            AddCategoryWindow window = new AddCategoryWindow(User.Id, Category?.Id);
            window.CategoryAdded += (sender, args) => Inventory.Add(new CategoryItem(args.Category));
            window.ShowDialog();
        }

        void OpenAddImageWindow()
        {
            AddImageWindow window = new AddImageWindow(Category.Id);
            window.ImageAdded += (sender, args) => Inventory.Add(new ImageItem(args.Image));
            window.ShowDialog();
        }

        void OpenEditItemWindow()
        {
            switch (SelectedItem)
            {
                case CategoryItem categoryItem:
                    OpenEditCategoryWindow(categoryItem.Category);
                    break;
                case ImageItem imageItem:
                    OpenEditImageWindow(imageItem.Image);
                    break;
                default: throw new ArgumentException("Unsupported item type provided.");
            }
        }

        void OpenEditCategoryWindow(Category category)
        {
            EditCategoryWindow window = new EditCategoryWindow(category);
            window.CategoryEdited += (sender, args) => ReplaceSelectedItem(new CategoryItem(args.Category));
            window.ShowDialog();
        }

        void OpenEditImageWindow(Image image)
        {
            EditImageWindow window = new EditImageWindow(image);
            window.ImageEdited += (sender, args) => ReplaceSelectedItem(new ImageItem(args.Image));
            window.ShowDialog();
        }

        void ReplaceSelectedItem(InventoryItem newItem)
        {
            Inventory[Inventory.IndexOf(SelectedItem)] = newItem;
        }

        void OpenDeleteItemWindow()
        {
            switch (SelectedItem)
            {
                case CategoryItem categoryItem:
                    CategoriesManager.Delete(categoryItem.Category.Id);
                    break;
                case ImageItem imageItem:
                    ImagesManager.Delete(imageItem.Image.Id);
                    break;
                default: throw new ArgumentException("Unsupported item type provided.");
            }

            Inventory.Remove(SelectedItem);
        }

        void UpdateInventory(CategoryWithContent category)
        {
            Inventory.Clear();
            category.Categories.ForEach(c => Inventory.Add(new CategoryItem(c)));
            category.Images.ForEach(i => Inventory.Add(new ImageItem(i)));
        }
    }

    public abstract class InventoryItem
    {
        public string Name { get; }
        public object ImageSource { get; }

        internal InventoryItem(string name, string imageSource)
        {
            Name = name;
            ImageSource = imageSource;
        }

        internal InventoryItem(string name, byte[] imageSource)
        {
            Name = name;
            ImageSource = imageSource;
        }
    }

    class CategoryItem : InventoryItem
    {
        const string FOLDER_IMAGE_PATH = @"..\images\folder-icon.png";

        internal Category Category { get; }

        internal CategoryItem(Category category) : base(category.Name, FOLDER_IMAGE_PATH)
        {
            Category = category;
        }
    }

    class ImageItem : InventoryItem
    {
        internal Image Image { get; }

        internal ImageItem(Image image) : base(image.Name, image.Data)
        {
            Image = image;
        }
    }
}
