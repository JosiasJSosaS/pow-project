namespace pow_project.Server.ExternalModels
{
    public class Movie
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public List<int>? genre_ids { get; set; }
        public string title { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
    }
}
