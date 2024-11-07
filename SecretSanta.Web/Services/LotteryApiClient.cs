using SecretSanta.Web.Models;

namespace SecretSanta.Web.Services;

public class LotteryApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;


    public async Task Sortear(long id, List<Amigo> amigos, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/Lottery?id={id}"
            , amigos
        , cancellationToken);

        // Optionally check if the response was successful or handle it as needed
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("API call failed.");
        }
    }
}