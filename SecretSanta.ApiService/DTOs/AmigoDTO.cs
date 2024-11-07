namespace SecretSanta.ApiService.DTOs;

public class AmigoDTO
{
    public AmigoDTO() { }

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public AmigoDTO(AmigoDTO otherAmigo)
    {
        Nome = otherAmigo.Nome;
        Email = otherAmigo.Email;
    }
}
