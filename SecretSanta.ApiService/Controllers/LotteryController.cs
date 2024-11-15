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
        int i = 0;
        List<PairFriendDTO> drawn = DrawPairs(model);

        foreach (var entry in drawn)
        {
            Console.WriteLine("Mail #" + (++i));
            _mailService.SendMail(entry.Giver.Email, entry.Giver.Name, entry.Receiver.Email, entry.Receiver.Name);
        }
        return Results.Ok("Emails sent");
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
        Console.WriteLine("Number of pairs: #" + pairs.Count);
        return pairs;
    }
}
