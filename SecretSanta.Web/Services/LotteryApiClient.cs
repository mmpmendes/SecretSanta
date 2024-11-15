using SecretSanta.Web.Models;

namespace SecretSanta.Web.Services;

public class LotteryApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;


    public async Task Draw(long id, List<Friend> friends, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/Lottery?id={id}"
            , friends
        , cancellationToken);

        // Optionally check if the response was successful or handle it as needed
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("API call failed.");
        }
    }
}