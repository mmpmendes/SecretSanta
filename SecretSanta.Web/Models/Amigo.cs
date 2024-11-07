using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Models;

public class Amigo
{
    public Amigo() { }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    public Amigo(Amigo otherAmigo)
    {
        Nome = otherAmigo.Nome;
        Email = otherAmigo.Email;
    }
}
