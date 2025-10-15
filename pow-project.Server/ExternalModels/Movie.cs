namespace pow_project.Server.ExternalModels
{
    public class Movie
    {
        int id { get; set; }
        bool adult { get; set; }
        List<int>? genre_ids { get; set; }
        string title { get; set; }
        string original_language { get; set; }
        string original_title { get; set; }
        string overview { get; set; }
        double popularity { get; set; }
        string poster_path { get; set; }
        string release_date { get; set; }
    }
}
