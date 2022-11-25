using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bacit_dotnet.MVC.Tests.TestRepositories
{

    public class TestSqlSuggestionsRepository : ISuggestionRepository
    {
         public void AddSuggestion(SuggestionEntity suggestion)
         {
             return;
         }
         public List<SuggestionEntity> GetSuggestions()
         {
             var suggestions = new List<SuggestionEntity>();
             suggestions.Add(new SuggestionEntity { SuggestionID = 1 });
             suggestions.Add(new SuggestionEntity { SuggestionID = 2 });
             return suggestions;
         }
         public void Delete(int Suggestion)
         {
             return;
         }
         public void Edit(SuggestionEntity suggestion)
         {
             return;
         }

         public List<SuggestionEntity> GetSuggestionsWithSearchQyery(string searchWord)
         {
             throw new NotImplementedException();
         }
    }
}
