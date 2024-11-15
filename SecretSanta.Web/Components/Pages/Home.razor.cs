using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using SecretSanta.Web.Models;

using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Components.Pages;

public partial class Home
{
    private int _nFriends = 3; // por defeito no mínimo 3


    [SupplyParameterFromForm]
    public List<Friend> Friends { get; set; } = [];
    public List<PairFriends> Pairs { get; set; } = [];

    [Required]
    public int NFriends
    {
        get => _nFriends;
        set
        {
            if (_nFriends != value)
            {
                _nFriends = value;
                UpdateFriends();
            }
        }
    }

    private CustomValidation? customValidation;
    //private int num_index = 0;

    private void UpdateFriends()
    {
        // Adjust the list size based on the input value
        if (Friends.Count > NFriends)
        {
            Friends.RemoveRange(NFriends, Friends.Count - NFriends);
        }
        else
        {
            while (Friends.Count < NFriends)
            {
                Friends.Add(new Friend());
            }
        }
        //num_index = 0;
        // Call StateHasChanged to refresh the UI after list modification
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        customValidation = new CustomValidation();
        UpdateFriends();
    }
    private void StepDownFriendsNumber(MouseEventArgs e)
    {
        if (NFriends > 3)
        {
            NFriends--;
            StateHasChanged();
        }
    }
    private void StepUpFriendsNumber(MouseEventArgs e)
    {
        NFriends++;
        StateHasChanged();
    }

    private async void Draw()
    {
        customValidation?.ClearErrors();

        var errors = new Dictionary<string, List<string>>();

        var i = 0;
        foreach (var friend in Friends)
        {
            i++;
            if (string.IsNullOrEmpty(friend!.Name))
            {
                errors.Add(nameof(friend.Name) + "_" + i,
                    new() { "Amigo " + i + ": Nome é obrigatório" });
            }

            if (string.IsNullOrEmpty(friend!.Email))
            {
                errors.Add(nameof(friend.Email) + "_" + i,
                    new() { "Amigo " + i + ": Email é obrigatório" });
            }
        }

        if (errors.Any())
        {
            customValidation?.DisplayErrors(errors);
            Console.WriteLine("errors");
        }
        else
        {
            Console.WriteLine("no errors ehy");

            await lotteryApi.Draw(123, Friends);
        }
    }
}
