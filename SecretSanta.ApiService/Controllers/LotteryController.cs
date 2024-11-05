using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using SecretSanta.ApiService.DTOs;
using SecretSanta.ApiService.Services.Email;
using SecretSanta.Models.Models;

using System.Net.Mail;

namespace SecretSanta.ApiService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LotteryController(
    IMapper mapper,
    EmailService emailService
    ) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly EmailService _emailService = emailService;

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

    private async Task<IResult> SendSantaEmail(string recipientEmail, string giverName, string receiverName, string receiverEmail)
    {
        try
        {
            await _emailService.SendSecretSantaEmailAsync(recipientEmail, giverName, receiverName, receiverEmail);
        }
        catch (SmtpException ex)
        {
            // Log or handle email sending failure
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
        return Results.Ok("Secret Santa email sent successfully"); ;
    }
}
