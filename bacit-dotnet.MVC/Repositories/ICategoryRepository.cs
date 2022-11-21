using bacit_dotnet.MVC.Models.Category;

namespace bacit_dotnet.MVC.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryEntity> GetCategories();
    }
}
