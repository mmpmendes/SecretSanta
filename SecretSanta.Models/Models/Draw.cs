using SecretSanta.MigrationService.Models;

using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Models.Models;
public class Draw
{
    public long Id { get; set; }
    public List<User> Users { get; set; }

    [NotMapped]
    public List<DrawEntry> Drawn { get; set; } = new List<DrawEntry>();
}
