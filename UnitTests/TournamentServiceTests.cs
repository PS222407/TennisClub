using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using UnitTests.Repositories;

namespace UnitTests;

public class TournamentServiceTests
{
    private ITournamentService? _tournamentService;

    [SetUp]
    public void Setup()
    {
        _tournamentService = new TournamentService(new TournamentRepository(), new UserRepository());
    }

    [Test]
    public void Gets_tournament_by_id_successfully()
    {
        // Arrange & Act
        Tournament? tournament = _tournamentService!.FindById(2);

        // Assert
        Assert.That(tournament!.Name, Is.EqualTo("ToernooiNaam2"));
    }

    [Test]
    public void Gets_tournament_by_id_fails()
    {
        // Arrange & Act
        Tournament? tournament = _tournamentService!.FindById(233);

        // Assert
        Assert.IsNull(tournament);
    }

    [Test]
    public void Gets_all_tournaments_successfully()
    {
        // Arrange & Act
        List<Tournament>? tournaments = _tournamentService!.GetAll();

        // Assert
        Assert.That(tournaments!.Count, Is.EqualTo(2));
    }

    [Test]
    public void Create_tournament_successfully()
    {
        // Arrange
        Tournament tournamentToCreate = new()
        {
            Id = 1,
            Name = "ToernooiNaam",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = DateTime.Today.AddDays(1),
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
        };

        // Act
        bool result = _tournamentService!.Create(tournamentToCreate);

        // Assert
        Assert.True(result);
    }

    [Test]
    public void Edit_tournament_successfully()
    {
        // Arrange
        Tournament tournamentToEdit = new()
        {
            Id = 2,
            Name = "ToernooiNaam",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = DateTime.Today.AddDays(1),
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
        };

        // Act
        bool result = _tournamentService!.Edit(2, tournamentToEdit);

        Assert.True(result);
    }

    [Test]
    public void Edit_tournament_fails()
    {
        // Arrange
        Tournament tournamentToEdit = new()
        {
            Id = 2,
            Name = "ToernooiNaam",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = DateTime.Today.AddDays(1),
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
        };

        // Act
        bool result = _tournamentService!.Edit(24545, tournamentToEdit);

        Assert.False(result);
    }

    [Test]
    public void Delete_tournament_successfully()
    {
        // Arrange & Act
        bool result = _tournamentService!.Delete(2);

        // Assert
        Assert.True(result);
    }

    [Test]
    public void Add_user_to_tournament_successfully()
    {
        // Arrange & Act
        StatusMessage statusMessage = _tournamentService!.AddUser(1, "1");

        // Assert
        Assert.True(statusMessage.Success);
    }

    [Test]
    public void Add_user_to_tournament_fails()
    {
        // Arrange & Act
        StatusMessage statusMessage = _tournamentService!.AddUser(132, "1");

        // Assert
        Assert.False(statusMessage.Success);
    }
}