using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using DotNet6API_Sample.Library.Interfaces;
using DotNet6API_Sample.Library.Models;
using DotNet6API_Sample.Library.Services;
using DotNet6API_Sample.Tests.Unit.Helpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace DotNet6API_Sample.Tests.Unit;

public class UserServiceTests
{
    [Theory]
    [AutoDomainData]
    [Trait("Category", "Unit")]
    public void UserService_GetUser_ReturnsUser(
        [Frozen] Mock<IUserRepository> userRepository,
        UserService sut,
        UserRecord userRecord
    )
    {
        // Arrange
        var userID = Guid.NewGuid();
        userRecord.ID = userID;
        userRepository.Setup(x => x.ReadByUserId(It.IsAny<Guid>())).Returns(userRecord);

        // Act
        var result = sut.GetUser(userID);

        // Assert
        result.Should().NotBeNull();
        result?.ID.Should().Be(userRecord.ID);
        result?.FirstName.Should().Be(userRecord.FirstName);
        result?.LastName.Should().Be(userRecord.LastName);
        result?.EmailAddress.Should().Be(userRecord.EmailAddress);

        userRepository.Verify(x => x.ReadByUserId(userID), Times.Once);
    }

    [Theory]
    [AutoDomainData]
    [Trait("Category", "Unit")]
    public void UserService_GetUsers_ReturnUsers(
        [Frozen] Mock<IUserRepository> userRepository,
        UserService sut,
        IReadOnlyCollection<UserRecord> userRecords
    )
    {
        // Arrange
        userRepository.Setup(x => x.ReadAll()).Returns(userRecords);

        // Act
        var result = sut.GetUsers();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);

        userRepository.Verify(x => x.ReadAll(), Times.Once);
    }

    [Theory]
    [AutoDomainData]
    [Trait("Category", "Unit")]
    public void UserService_SaveUserWithNoID_ReturnsIDSaved(
        [Frozen] Mock<IUserRepository> userRepository,
        UserService sut,
        UserRecord userRecord
    )
    {
        // Arrange
        var userID = Guid.NewGuid();
        userRecord.ID = null;
        userRepository.Setup(x => x.Create(It.IsAny<UserRecord>())).Returns(userID);

        // Act
        var result = sut.SaveUser(userRecord);

        // Assert
        result.Should().NotBe(Guid.Empty);
        result.Should().Be(userID);

        userRepository.Verify(x => x.Create(It.IsAny<UserRecord>()), Times.Once);
        userRepository.Verify(x => x.Update(It.IsAny<UserRecord>()), Times.Never);
    }

    [Theory]
    [AutoDomainData]
    [Trait("Category", "Unit")]
    public void UserService_SaveUserWithEmptyGuidID_ReturnsIDSaved(
        [Frozen] Mock<IUserRepository> userRepository,
        UserService sut,
        UserRecord userRecord
    )
    {
        // Arrange
        var userID = Guid.NewGuid();
        userRecord.ID = Guid.Empty;
        userRepository.Setup(x => x.Create(It.IsAny<UserRecord>())).Returns(userID);

        // Act
        var result = sut.SaveUser(userRecord);

        // Assert
        result.Should().NotBe(Guid.Empty);
        result.Should().Be(userID);

        userRepository.Verify(x => x.Create(It.IsAny<UserRecord>()), Times.Once);
        userRepository.Verify(x => x.Update(It.IsAny<UserRecord>()), Times.Never);
    }


    [Theory]
    [AutoDomainData]
    [Trait("Category", "Unit")]
    public void UserService_SaveUserWithID_ReturnsUserID(
        [Frozen] Mock<IUserRepository> userRepository,
        UserService sut,
        UserRecord userRecord
    )
    {
        // Arrange
        var userID = Guid.NewGuid();
        userRepository.Setup(x => x.Update(It.IsAny<UserRecord>())).Returns(userID);

        // Act
        var result = sut.SaveUser(userRecord);

        // Assert
        result.Should().NotBe(Guid.Empty);
        result.Should().Be(userID);

        userRepository.Verify(x => x.Update(It.IsAny<UserRecord>()), Times.Once);
        userRepository.Verify(x => x.Create(It.IsAny<UserRecord>()), Times.Never);
    }
}