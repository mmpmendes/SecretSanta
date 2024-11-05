namespace SecretSanta.Models.Models;
public class DrawEntry
{
    public long Id { get; set; }
    public string GiverName { get; set; }
    public string GiverEmail { get; set; }
    public long GiverId { get; set; }
    public long ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }
}
