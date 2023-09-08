using DataLayer.Dtos;

namespace BusinessLogicLayer.Interfaces;

public interface ICourtService
{
    public List<CourtDto> GetAll();

    public CourtDto? FindById(int id);

    public bool Create(CourtDto courtDto);

    public bool Edit(int id, CourtDto courtDto);

    public bool Delete(int id);
}