using Microsoft.AspNetCore.Components.Forms;

using SecretSanta.Web.Models;

using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.Components.Pages;

public partial class Home
{
    private int n_amigos = 3; // por defeito no mínimo 3
    public List<Amigo> Amigos { get; set; } = new List<Amigo>();
    public List<ParAmigos> pares { get; set; } = new List<ParAmigos>();

    [Required]
    public int nAmigos
    {
        get => n_amigos;
        set
        {
            if (n_amigos != value)
            {
                n_amigos = value;
                UpdateAmigos();
            }
        }
    }

    private void UpdateAmigos()
    {
        // Adjust the list size based on the input value
        if (Amigos.Count > nAmigos)
        {
            Amigos.RemoveRange(nAmigos, Amigos.Count - nAmigos);
        }
        else
        {
            while (Amigos.Count < nAmigos)
            {
                Amigos.Add(new Amigo());
            }
        }
        // Call StateHasChanged to refresh the UI after list modification
        StateHasChanged();
    }



    protected override void OnInitialized() => UpdateAmigos();
    private void StepDownFriendsNumber(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        nAmigos--;
        StateHasChanged();
    }
    private void StepUpFriendsNumber(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        nAmigos++;
        StateHasChanged();
    }

    private async Task sortear()
    {

        await lotteryApi.Sortear(123, Amigos);
    }
    private void HandleSubmit()
    {
        var editContext = new EditContext(Amigos);
        if (!editContext.Validate())
        {
            return;
        }

        sortear();
    }
}
