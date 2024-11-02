using SecretSanta.MigrationService.Models;

namespace SecretSanta.Models.Models;
public class DrawEntry
{
    public long Id { get; set; }
    public User Giver { get; set; }
    public long GiverId { get; set; }
    public User Receiver { get; set; }
    public long ReceiverId { get; set; }
}
