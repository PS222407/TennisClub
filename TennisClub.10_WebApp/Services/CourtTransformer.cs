using BusinessLogicLayer.Models;
using TennisClub_0._1.Requests;
using TennisClub_0._1.ViewModels;

namespace TennisClub_0._1.Services;

public class CourtTransformer
{
    public List<CourtViewModel> ModelsToViews(List<Court> courts)
    {
        return courts.Select(ModelToView).ToList();
    }

    public CourtViewModel ModelToView(Court court)
    {
        return new CourtViewModel
        {
            Id = court.Id,
            Double = court.Double,
            Indoor = court.Indoor,
            Number = court.Number,
        };
    }

    public Court RequestToModel(CourtRequest courtRequest)
    {
        return new Court
        {
            Double = courtRequest.Double,
            Indoor = courtRequest.Indoor,
            Number = courtRequest.Number,
        };
    }
}