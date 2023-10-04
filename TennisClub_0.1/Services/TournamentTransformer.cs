using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;
using Court = BusinessLogicLayer.Models.Court;
using Tournament = BusinessLogicLayer.Models.Tournament;
using User = BusinessLogicLayer.Models.User;

namespace TennisClub_0._1.Services;

public class TournamentTransformer
{
    public List<TournamentViewModel> ModelsToViews(List<Tournament> tournamentDtos)
    {
        return tournamentDtos.Select(ModelToView).ToList();
    }

    public TournamentViewModel ModelToView(Tournament tournament)
    {
        TournamentViewModel tournamentViewModel = new()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Description = tournament.Description,
            Price = tournament.Price,
            MaxMembers = tournament.MaxMembers,
            StartDateTime = tournament.StartDateTime,
            ImageUrl = tournament.ImageUrl,
        };

        List<CourtViewModel> courtViewModels = new();
        foreach (Court court in tournament.Courts)
        {
            courtViewModels.Add(new CourtViewModel
            {
                Id = court.Id,
                Number = court.Number,
                Indoor = court.Indoor,
                Double = court.Double,
            });
        }

        tournamentViewModel.Courts = courtViewModels;

        List<UserViewModel> userViewModels = new();
        foreach (User user in tournament.Users)
        {
            userViewModels.Add(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
            });
        }

        tournamentViewModel.Participants = userViewModels;

        return tournamentViewModel;
    }

    public TournamentViewModel RequestToView(TournamentRequest tournamentRequest)
    {
        return new TournamentViewModel
        {
            Id = tournamentRequest.Id,
            MaxMembers = tournamentRequest.MaxMembers,
            Name = tournamentRequest.Name,
            Description = tournamentRequest.Description,
            Price = tournamentRequest.Price,
            StartDateTime = tournamentRequest.StartDateTime,
            SelectedCourtIds = tournamentRequest.SelectedCourtIds,
            CourtOptions = tournamentRequest.CourtOptions,
        };
    }

    public Tournament RequestToDto(TournamentRequest tournamentRequest)
    {
        return new Tournament
        {
            Name = tournamentRequest.Name,
            Description = tournamentRequest.Description,
            Price = tournamentRequest.Price,
            MaxMembers = tournamentRequest.MaxMembers,
            StartDateTime = tournamentRequest.StartDateTime,
            CourtIds = tournamentRequest.SelectedCourtIds,
        };
    }
}