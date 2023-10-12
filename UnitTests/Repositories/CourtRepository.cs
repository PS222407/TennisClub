using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;

namespace UnitTests.Repositories;

public class CourtRepository : ICourtRepository
{
    private List<Court> _courts = new()
    {
        new Court
        {
            Id = 1,
            Double = true,
            Indoor = true,
            Number = 1,
        },
        new Court
        {
            Id = 2,
            Double = true,
            Indoor = false,
            Number = 2,
        },
        new Court
        {
            Id = 3,
            Double = false,
            Indoor = true,
            Number = 3,
        },
        new Court
        {
            Id = 4,
            Double = false,
            Indoor = false,
            Number = 4,
        },
    };

    public List<Court>? GetAll()
    {
        return _courts.OrderBy(c => c.Id).ToList();
    }

    public Court? FindById(int id)
    {
        return _courts.FirstOrDefault(c => c.Id == id);
    }

    public bool Create(Court court)
    {
        Court? lastCourt = _courts.LastOrDefault();
        court.Id = lastCourt == null ? 1 : lastCourt.Id + 1;

        _courts.Add(court);
        return true;
    }

    public bool Edit(int id, Court court)
    {
        Court? courtFromList = _courts.FirstOrDefault(c => c.Id == id);
        if (courtFromList == null)
        {
            return false;
        }

        _courts.Remove(courtFromList);
        _courts.Add(court);
        return true;
    }

    public bool Delete(int id)
    {
        Court? courtFromList = _courts.FirstOrDefault(c => c.Id == id);
        if (courtFromList == null)
        {
            return false;
        }

        _courts.Remove(courtFromList);
        return true;
    }

    public bool Exists(int id)
    {
        return _courts.Exists(c => c.Id == id);
    }
}