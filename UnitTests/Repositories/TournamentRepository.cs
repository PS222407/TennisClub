using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;

namespace UnitTests.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private List<Tournament> _tournaments = new()
    {
        new Tournament
        {
            Id = 1,
            Name = "ToernooiNaam",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = new DateTime(),
            Price = 1000,
            ImageUrl = "https://www.google.com",
            Users = new List<User>
            {
                new()
                {
                    Id = "1",
                    UserName = "Gebruikersnaam",
                    Email = "Email@mail.mail",
                },
                new()
                {
                    Id = "2",
                    UserName = "Gebruikersnaam 2",
                    Email = "Email@mail.mail2",
                },
            },
            Courts = new List<Court>
            {
                new()
                {
                    Id = 1,
                    Double = true,
                    Indoor = true,
                    Number = 1,
                },
                new()
                {
                    Id = 2,
                    Double = true,
                    Indoor = false,
                    Number = 2,
                },
                new()
                {
                    Id = 3,
                    Double = false,
                    Indoor = true,
                    Number = 3,
                },
                new()
                {
                    Id = 4,
                    Double = false,
                    Indoor = false,
                    Number = 4,
                },
            },
            CourtIds = new List<int>
            {
                1, 2, 3, 4,
            },
        },
        new Tournament
        {
            Id = 2,
            Name = "ToernooiNaam2",
            Description = "Beschrijving2",
            MaxMembers = 20,
            StartDateTime = new DateTime(),
            Price = 2000,
            ImageUrl = "https://www.google.com",
            Users = new List<User>
            {
                new()
                {
                    Id = "1",
                    UserName = "Gebruikersnaam",
                    Email = "Email@mail.mail",
                },
                new()
                {
                    Id = "2",
                    UserName = "Gebruikersnaam 2",
                    Email = "Email@mail.mail2",
                },
            },
            Courts = new List<Court>
            {
                new()
                {
                    Id = 1,
                    Double = true,
                    Indoor = true,
                    Number = 1,
                },
                new()
                {
                    Id = 2,
                    Double = true,
                    Indoor = false,
                    Number = 2,
                },
                new()
                {
                    Id = 3,
                    Double = false,
                    Indoor = true,
                    Number = 3,
                },
                new()
                {
                    Id = 4,
                    Double = false,
                    Indoor = false,
                    Number = 4,
                },
            },
            CourtIds = new List<int>
            {
                1, 2, 3, 4,
            },
        },
    };

    public List<Tournament>? GetAll()
    {
        return _tournaments;
    }

    public Tournament? FindById(int id)
    {
        return _tournaments.FirstOrDefault(t => t.Id == id);
    }

    public bool Create(Tournament tournament)
    {
        _tournaments.Add(tournament);
        return true;
    }

    public bool Edit(int id, Tournament tournament)
    {
        Tournament? tournamentFromList = _tournaments.FirstOrDefault(t => t.Id == id);
        if (tournamentFromList == null)
        {
            return false;
        }

        _tournaments.Remove(tournamentFromList);
        _tournaments.Add(tournament);
        return true;
    }

    public bool Delete(int id)
    {
        Tournament? tournamentFromList = _tournaments.FirstOrDefault(t => t.Id == id);
        if (tournamentFromList == null)
        {
            return false;
        }

        _tournaments.Remove(tournamentFromList);
        return true;
    }

    public StatusMessage AddUser(int tournamentId, string userId)
    {
        Tournament? tournamentFromList = _tournaments.FirstOrDefault(t => t.Id == tournamentId);
        if (tournamentFromList == null)
        {
            return new StatusMessage
            {
                Success = false,
                Reason = "Record not found",
            };
        }

        tournamentFromList.Users.Add(new User
        {
            Id = userId,
            UserName = "username",
            Email = "email",
        });

        return new StatusMessage
        {
            Success = true,
            Reason = "Successfully stored in database",
        };
    }

    public bool Exists(int id)
    {
        return _tournaments.Exists(t => t.Id == id);
    }
}