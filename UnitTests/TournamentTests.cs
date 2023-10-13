using BusinessLogicLayer.Models;

namespace UnitTests;

public class TournamentTests
{
    [Test]
    public void Tournament_validates_successfully()
    {
        // Arrange
        Tournament tournament = new()
        {
            Id = 1,
            Name = "ToernooiNaam",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = DateTime.Today.AddDays(1),
            Price = 1020,
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
        bool result = tournament.IsValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Tournament_validates_fails()
    {
        // Arrange
        Tournament tournament = new()
        {
            Id = 1,
            Name = " ",
            Description = "Beschrijving",
            MaxMembers = 10,
            StartDateTime = DateTime.Today.AddDays(-1),
            Price = 1020,
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
        bool result = tournament.IsValid();

        // Assert
        Assert.That(result, Is.False);
    }
}