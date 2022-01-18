using System;
using System.Linq;
using DotNet6API_Sample.Library.Models;
using DotNet6API_Sample.Library.Repositories;
using FluentAssertions;
using Xunit;

namespace DotNet6API_Sample.Tests.Integration;

public class EntityFrameworkUserRepositoryTests
{
    private readonly EntityFrameworkUserRepository _sut;

    public EntityFrameworkUserRepositoryTests()
    {
        var userDbContext =
            new UserDbContext(
                "Server=localhost,1433;Database=dotnet6-api-sample;user id=sa;password=P@ssw0rd;");

        _sut = new EntityFrameworkUserRepository(userDbContext);
    }

    [Fact]
    public void EntityFrameworkUserRepository_ReadAll_ReturnsAllUsers()
    {
        // Act
        var result = _sut.ReadAll();

        // Assert
        result.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void EntityFrameworkUserRepository_ReadByID_ReturnsSingleUser()
    {
        // Arrange
        var user = _sut.ReadAll().First();
        var userID = user.ID ?? throw new NullReferenceException("User ID should not be null");

        // Act
        var result = _sut.ReadByUserId(userID);

        // Assert
        result.Should().NotBeNull();
        result?.ID.Should().Be(userID);
    }

    [Fact]
    public void EntityFrameworkUserRepository_Create_CreatesUser()
    {
        // Arrange
        var user = new UserRecord
        {
            FirstName = "Test",
            LastName = "User",
            EmailAddress = "test@user.com"
        };

        // Act
        var result = _sut.Create(user);

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void EntityFrameworkUserRepository_Update_CreatesUser()
    {
        // Arrange
        var user = new UserRecord
        {
            FirstName = "Test",
            LastName = "User",
            EmailAddress = "test@user.com"
        };

        const string updatedFirstName = "Updated";

        // Act
        var recordToUpdateID = _sut.Create(user);

        var recordToUpdate = _sut.ReadByUserId(recordToUpdateID);

        if (recordToUpdate == null) throw new NullReferenceException("Record to update should not be null");

        recordToUpdate.FirstName = updatedFirstName;

        _sut.Update(recordToUpdate);

        var result = _sut.ReadByUserId(recordToUpdateID);

        // Assert
        result.Should().NotBeNull();
        result?.FirstName.Should().Be(updatedFirstName);
    }
}