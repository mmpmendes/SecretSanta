using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Models;

public class Friend
{
    public Friend() { }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório")]
    public string Name { get; set; } = String.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = String.Empty;

    public Friend(Friend otherAmigo)
    {
        Name = otherAmigo.Name;
        Email = otherAmigo.Email;
    }
}
