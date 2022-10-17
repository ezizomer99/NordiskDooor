using bacit_dotnet.MVC.Entities;

namespace bacit_dotnet.MVC.Models.Suggestions
{
    public class SuggestionViewModel
    {
       
        public int SuggestionID { get; set; }
        public string SuggestionMakerID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }
        public string Phase { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Deadline { get; set; }
        public List<SuggestionEntity> Suggestions { get; set; }
    }
}
