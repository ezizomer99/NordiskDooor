using bacit_dotnet.MVC.DataAccess;
using bacit_dotnet.MVC.Repositories.Misc;
using bacit_dotnet.MVC.Models.Category;
using System.Data;

namespace bacit_dotnet.MVC.Repositories
{
    public class SqlCategoryRepository : ICategoryRepository
    {

        private readonly ISqlConnector sqlConnector;

        public SqlCategoryRepository(ISqlConnector sqlConnector)
        {
            this.sqlConnector = sqlConnector;
        }

        public List<CategoryEntity> GetCategories()
        {
            using (var connection = sqlConnector.GetDbConnection())
            {
                var reader = Command.ReadData("Select CategoryId, CategoryName from Category;", connection);
                var categories = new List<CategoryEntity>();
                while (reader.Read())
                {
                    CategoryEntity category = MapCategoryFromReader(reader);
                    categories.Add(category);
                }
                connection.Close();
                return categories;
            }
        }

        private static CategoryEntity MapCategoryFromReader(IDataReader reader)
        {
            var category = new CategoryEntity();
            category.CategoryId = reader.GetInt16(0);
            category.CategoryName = reader.GetString(1);
            return category;
        }
    }
}
