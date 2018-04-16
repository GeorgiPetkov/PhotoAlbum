namespace Logic
{
    public static class Services
    {
        public static IUsersManager UsersManager { get; } = new UsersManager();
        public static ICategoriesManager CategoriesManager { get; } = new CategoriesManager();
        public static IImagesManager ImagesManager { get; } = new ImagesManager();
    }
}
