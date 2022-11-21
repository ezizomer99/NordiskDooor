using bacit_dotnet.MVC.Models.Category;
using bacit_dotnet.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.TestRepositories
{
    internal class TestSqlCategoryRepository : ICategoryRepository
    {
        public List<CategoryEntity> GetCategories()
        {
            var categories = new List<CategoryEntity>();
            return categories;
        }
    }
}
