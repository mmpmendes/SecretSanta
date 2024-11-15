namespace SecretSanta.ApiService.DTOs;

public class FriendDTO
{
    public FriendDTO() { }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public FriendDTO(FriendDTO otherFriend)
    {
        Name = otherFriend.Name;
        Email = otherFriend.Email;
    }
}
