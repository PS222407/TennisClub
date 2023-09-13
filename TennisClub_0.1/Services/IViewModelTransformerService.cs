using DataLayer.Dtos;
using TennisClub_0._1.Models;

namespace TennisClub_0._1.Services;

public interface IViewModelTransformerService
{
    public List<TournamentViewModel> TransformTournaments(List<TournamentDto> tournamentDtos);
    
    public TournamentViewModel TransformTournament(TournamentDto tournamentDto);
    
    public List<CourtDto> TransformCourts(List<CourtDto> courtDtos);
    
    public CourtDto TransformCourt(CourtDto courtDto);
}