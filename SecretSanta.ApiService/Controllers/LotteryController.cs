using Microsoft.AspNetCore.Mvc;

using SecretSanta.ApiService.DTOs;
using SecretSanta.ApiService.Services.Email;

namespace SecretSanta.ApiService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LotteryController(IMailService mailService) : ControllerBase
{
    private readonly IMailService _mailService = mailService;

    [HttpPost]
    public async Task<IResult> Draw(int id, [FromBody] IEnumerable<FriendDTO> model)
    {
        if (model == null)
            return Results.BadRequest();

        List<PairFriendDTO> drawn = DrawPairs(model);

        foreach (var entry in drawn)
        {
            Console.WriteLine("Before SendSantaEmail: " + entry.Giver.Email);
            SendSantaEmail(entry.Giver.Email, entry.Giver.Name, entry.Receiver.Email, entry.Receiver.Name);
        }
        return Results.Ok("Emails sent");
    }

    private IResult SendSantaEmail(string giverEmail, string giverName, string receiverEmail, string receiverName)
    {
        try
        {
            _mailService.SendMail(giverEmail, giverName, receiverEmail, receiverName);
        }
        catch (Exception ex)
        {
            // Log or handle email sending failure
            Console.WriteLine($"Failed to send email: {ex.Message} - " + ex.InnerException);
        }
        return Results.Ok(); ;
    }

    private List<PairFriendDTO> DrawPairs(IEnumerable<FriendDTO> friends)
    {
        var rnd = new Random();

        var friendsShake = new List<FriendDTO>();
        friendsShake = friends.OrderBy(_ => rnd.Next()).Select(item => new FriendDTO(item)).ToList();

        var pairs = new List<PairFriendDTO>();

        for (int i = 0; i < friendsShake.Count; i++)
        {
            pairs.Add(new PairFriendDTO()
            {
                Giver = friendsShake[i],
                Receiver = friendsShake[(i + 1) % friendsShake.Count]
            });
        }
        Console.WriteLine(pairs);
        return pairs;
    }
}
