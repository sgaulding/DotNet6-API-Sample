using System;
using DotNet6API_Sample.Library.Models;
using DotNet6API_Sample.Library.Services;
using FluentAssertions;
using Xunit;

namespace DotNet6API_Sample.Tests.Unit;

public class UserServiceTests
{
    [Fact]
    public void UserService_GetUser_ReturnsUser()
    {
        // Arrange
        var userID = new Guid("6EAEA9BB-8094-44FD-8124-6A3D809D7983");
        var sut = new UserService();

        // Act
        var result = sut.GetUser(userID);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userID);
        result.FistName.Should().Be("John");
        result.LastName.Should().Be("Doe");
        result.Email.Should().Be("john.doe@test.com");
    }

    [Fact]
    public void UserService_GetUsers_ReturnUsers()
    {
        // Arrange
        var sut = new UserService();

        // Act
        var result = sut.GetUsers();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(u => u.Id == new Guid("6EAEA9BB-8094-44FD-8124-6A3D809D7983"));
    }

    [Fact]
    public void UserService_SaveUserWithNoID_ReturnsIDSaved()
    {
        // Arrange
        var sut = new UserService();

        var user = new UserRecord
        {
            FistName = "John",
            LastName = "Doe",
            Email = "john.doe@test.com"
        };

        // Act
        var result = sut.SaveUser(user);

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void UserService_SaveUserWithID_ReturnsUserID()
    {
        // Arrange
        var userID = Guid.NewGuid();

        var sut = new UserService();

        var user = new UserRecord
        {
            Id = userID,
            FistName = "John",
            LastName = "Doe",
            Email = "john.doe@test.com"
        };

        // Act
        var result = sut.SaveUser(user);

        // Assert
        result.Should().NotBe(Guid.Empty);
        result.Should().Be(userID);
    }
}