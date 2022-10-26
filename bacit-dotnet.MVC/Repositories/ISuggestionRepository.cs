using bacit_dotnet.MVC.Models.Suggestions;

namespace bacit_dotnet.MVC.Repositories
{
    public interface ISuggestionRepository
    {
        void AddSuggestion(SuggestionEntity suggestion);
        List<SuggestionEntity> GetSuggestions();
        void Edit(int Suggestion);

        void Delete(int Suggestion);
    }
}
