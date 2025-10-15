namespace pow_project.Server.ExternalModels
{
    public class ResultPage
    {
        int page { get; set; }
        List<Movie>? results { get; set; }
        int total_pages { get; set; }
        int total_results { get; set; }
    }
}
