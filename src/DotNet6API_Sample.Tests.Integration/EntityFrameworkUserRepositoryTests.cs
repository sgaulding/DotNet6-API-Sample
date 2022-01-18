using System;
using System.IO;
using System.Linq;
using DotNet6API_Sample.Library.Interfaces;
using DotNet6API_Sample.Library.Models;
using DotNet6API_Sample.Library.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotNet6API_Sample.Tests.Integration;

public class EntityFrameworkUserRepositoryTests
{
    private readonly EntityFrameworkUserRepository? _sut;

    public EntityFrameworkUserRepositoryTests()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Path.GetFullPath("./appsettings.json"), false, true)
            .AddJsonFile(Path.GetFullPath("./appsettings.docker.json"), true, true)
            .AddEnvironmentVariables();

        var builder = configurationBuilder.Build();

        var dotnet6DbConnectionString = builder.GetSection("ConnectionStrings:dotnet6-api-sample-mssql").Value;

        var services = new ServiceCollection();
        services.AddOptions();
        services.AddSingleton<IConfiguration>(provider => builder);
        services.AddSingleton(new UserDbContext(dotnet6DbConnectionString));
        services.AddSingleton<IUserRepository, EntityFrameworkUserRepository>();
        var container = services.BuildServiceProvider();

        _sut = container.GetService<EntityFrameworkUserRepository>();
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