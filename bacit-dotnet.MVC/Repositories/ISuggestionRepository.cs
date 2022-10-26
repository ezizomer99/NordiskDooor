﻿using bacit_dotnet.MVC.Models.Suggestions;

namespace bacit_dotnet.MVC.Repositories
{
    public interface ISuggestionRepository
    {
        void AddSuggestion(SuggestionEntity suggestion);
        List<SuggestionEntity> GetSuggestions();
        void Delete(int Suggestion);
    }
}
