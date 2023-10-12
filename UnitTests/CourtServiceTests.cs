using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using FluentAssertions;
using UnitTests.Repositories;

namespace UnitTests;

public class CourtServiceTests
{
    private ICourtService? _courtService;

    [SetUp]
    public void Setup()
    {
        _courtService = new CourtService(new CourtRepository());
    }

    [Test]
    public void GetById_ReturnsCourt()
    {
        // Arrange
        Court expectedCourt = new()
        {
            Id = 3,
            Double = false,
            Indoor = true,
            Number = 3,
        };
        // Act
        Court? court = _courtService!.FindById(3);

        // Assert
        court.Should().BeEquivalentTo(expectedCourt);
    }

    [Test]
    public void GetAll_ReturnsCourts()
    {
        // Arrange
        List<Court> expectedCourts = new()
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

        // Act
        List<Court>? courts = _courtService!.GetAll();

        // Assert
        for (int i = 0; i < courts?.Count; i++)
        {
            Court court = courts[i];
            court.Should().BeEquivalentTo(expectedCourts[i]);
        }
    }

    [Test]
    public void Create_ReturnsCourt()
    {
        // Arrange
        Court courtToCreate = new()
        {
            Double = false,
            Indoor = false,
            Number = 4,
        };
        Court courtToCompare = new()
        {
            Id = 5,
            Double = false,
            Indoor = false,
            Number = 4,
        };

        // Act
        bool result = _courtService!.Create(courtToCreate);
        Court? createdCourt = _courtService!.FindById(5);

        // Assert
        Assert.That(result, Is.True);
        createdCourt.Should().BeEquivalentTo(courtToCompare);
    }

    [Test]
    public void Edit_ReturnsCourt()
    {
        // Arrange
        Court courtToEdit = new()
        {
            Id = 1,
            Double = true,
            Indoor = false,
            Number = 4,
        };

        // Act
        bool result = _courtService!.Edit(1, courtToEdit);
        Court? editedCourt = _courtService!.FindById(1);

        // Assert
        Assert.That(result, Is.True);
        editedCourt.Should().BeEquivalentTo(courtToEdit);
    }

    [Test]
    public void Delete_ReturnsTrue()
    {
        // Arrange & Act
        bool result = _courtService!.Delete(1);
        Court? deletedCourt = _courtService!.FindById(1);

        // Assert
        Assert.That(result && deletedCourt == null);
    }
}