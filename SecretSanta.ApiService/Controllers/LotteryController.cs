using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using SecretSanta.ApiService.DTOs;
using SecretSanta.ApiService.Services.Email;

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
    public async Task<IResult> Sortear(int id, [FromBody] IEnumerable<AmigoDTO> model)
    {
        if (model == null)
            return Results.BadRequest();

        List<ParAmigoDTO> sorteados = SortearPares(model);

        foreach (var entry in sorteados)
        {
            SendSantaEmail(entry.Dador.Email, entry.Dador.Nome, entry.Recebedor.Email, entry.Recebedor.Nome);
        }
        return Results.Ok("Emails Enviados");
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

    private List<ParAmigoDTO> SortearPares(IEnumerable<AmigoDTO> amigos)
    {
        var rnd = new Random();

        var amigosMisturados = new List<AmigoDTO>();
        amigosMisturados = amigos.OrderBy(_ => rnd.Next()).Select(item => new AmigoDTO(item)).ToList();

        var pares = new List<ParAmigoDTO>();

        for (int i = 0; i < amigosMisturados.Count; i++)
        {
            pares.Add(new ParAmigoDTO()
            {
                Dador = amigosMisturados[i],
                Recebedor = amigosMisturados[(i + 1) % amigosMisturados.Count]
            });
        }

        return pares;
    }
}
