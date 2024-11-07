using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using SecretSanta.ApiService.DTOs;
using SecretSanta.ApiService.Services.Email;
using SecretSanta.Models.Models;

namespace SecretSanta.ApiService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LotteryController(
    IMapper mapper,
    IMailService mailService

    ) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IMailService _mailService = mailService;

    [HttpPost]
    public async Task<IResult> SaveDraw(int id, [FromBody] IEnumerable<ParAmigoDTO> model)
    {
        if (model == null)
            return Results.BadRequest();

        var mapped = _mapper.Map<IEnumerable<DrawEntry>>(model);

        var entry = mapped.FirstOrDefault();

        if (entry != null)
            await SendSantaEmail(entry.ReceiverEmail, entry.GiverName, entry.ReceiverName, entry.ReceiverEmail);

        return Results.Ok();
    }

    private async Task<IResult> SendSantaEmail(string giverEmail, string giverName, string receiverEmail, string receiverName)
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
        return Results.Ok("Secret Santa email sent successfully"); ;
    }
}
