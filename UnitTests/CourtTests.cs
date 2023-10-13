using BusinessLogicLayer.Models;

namespace UnitTests;

public class CourtTests
{
    [Test]
    public void Court_validates_successfully()
    {
        // Arrange
        Court court = new()
        {
            Id = 1,
            Double = true,
            Indoor = true,
            Number = 1,
        };

        // Act
        bool isValid = court.IsValid();

        // Assert
        Assert.That(isValid, Is.True);
    }
    
    [Test]
    public void Court_validation_fails()
    {
        // Arrange
        Court court = new()
        {
            Id = 1,
            Double = true,
            Indoor = true,
            Number = -11,
        };

        // Act
        bool isValid = court.IsValid();

        // Assert
        Assert.That(isValid, Is.False);
    }
}