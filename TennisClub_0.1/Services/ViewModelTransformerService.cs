using DataLayer.Dtos;
using TennisClub_0._1.Models;

namespace TennisClub_0._1.Services;

public class ViewModelTransformerService : IViewModelTransformerService
{
    public List<TournamentViewModel> TransformTournaments(List<TournamentDto> tournamentDtos)
    {
        return tournamentDtos.Select(TransformTournament).ToList();
    }

    public TournamentViewModel TransformTournament(TournamentDto tournamentDto)
    {
        TournamentViewModel tournamentViewModel = new TournamentViewModel
        {
            Id = tournamentDto.Id,
            Name = tournamentDto.Name,
            Description = tournamentDto.Description,
            Price = tournamentDto.Price,
            MaxMembers = tournamentDto.MaxMembers,
            StartDateTime = tournamentDto.StartDateTime,
        };
            
        List<CourtViewModel> courtViewModels = new List<CourtViewModel>();
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
        
        List<UserViewModel> userViewModels = new List<UserViewModel>();
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

    public List<CourtDto> TransformCourts(List<CourtDto> courtDtos)
    {
        throw new NotImplementedException();
    }

    public CourtDto TransformCourt(CourtDto courtDto)
    {
        throw new NotImplementedException();
    }
}