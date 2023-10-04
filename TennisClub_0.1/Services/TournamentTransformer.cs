using DataLayer.Dtos;
using TennisClub_0._1.Models;
using TennisClub_0._1.Requests;

namespace TennisClub_0._1.Services;

public class TournamentTransformer
{
    public List<TournamentViewModel> DtosToViews(List<TournamentDto> tournamentDtos)
    {
        return tournamentDtos.Select(DtoToView).ToList();
    }

    public TournamentViewModel DtoToView(TournamentDto tournamentDto)
    {
        TournamentViewModel tournamentViewModel = new()
        {
            Id = tournamentDto.Id,
            Name = tournamentDto.Name,
            Description = tournamentDto.Description,
            Price = tournamentDto.Price,
            MaxMembers = tournamentDto.MaxMembers,
            StartDateTime = tournamentDto.StartDateTime,
            ImageUrl = tournamentDto.ImageUrl,
        };

        List<CourtViewModel> courtViewModels = new();
        foreach (CourtDto courtDto in tournamentDto.Courts)
        {
            courtViewModels.Add(new CourtViewModel
            {
                Id = courtDto.Id,
                Number = courtDto.Number,
                Indoor = courtDto.Indoor,
                Double = courtDto.Double,
            });
        }

        tournamentViewModel.Courts = courtViewModels;

        List<UserViewModel> userViewModels = new();
        foreach (UserDto userDto in tournamentDto.Users)
        {
            userViewModels.Add(new UserViewModel
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
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

    public TournamentDto RequestToDto(TournamentRequest tournamentRequest)
    {
        return new TournamentDto
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