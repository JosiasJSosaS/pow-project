using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pow_project.Server.ExternalModels;
using RestSharp;
using System.Text.Json;

namespace pow_project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteApiTest : ControllerBase
    {

        [HttpGet]
        public Task<IActionResult> GetMovies()
        {
            RestClientOptions options = new RestClientOptions("https://api.themoviedb.org/3/trending/movie/day?language=es-US");
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest();

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiZTBiMjBlMjMwZjQ1OGQxOTY2ODIwYmUzZTA5MWZiMyIsIm5iZiI6MTc1NjcyNjAzNS4yNjgsInN1YiI6IjY4YjU4MzEzZmNkNDdjMmVkOTNmMDVhNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.rvbbEySKWdWjY0G5wNhSSgRHOcbzHiwl8aBBFhOGZLk");

            Task<RestResponse>? response = client.GetAsync(request);

            return response.ContinueWith<IActionResult>(resp => 
            {
                if (resp.Result.IsSuccessful)
                {
                    string json = resp.Result.Content!;

                    var Movies = JsonSerializer.Deserialize<ResultPage>(json);

                    return Ok(Movies);
                }
                else
                {
                    return StatusCode((int)resp.Result.StatusCode, resp.Result.Content);
                }
            });
        }
    }
}
