namespace SecretSanta.Web.Models;

public class Amigo
{
    public Amigo() { }

    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Amigo? AmigoSorteado { get; set; }

    public Amigo(Amigo otherAmigo)
    {
        Nome = otherAmigo.Nome;
        Email = otherAmigo.Email;
        AmigoSorteado = otherAmigo.AmigoSorteado;
    }
}
