using System.Collections.ObjectModel;
using DotNet6API_Sample.Library.Models;

namespace DotNet6API_Sample.Library.Services;

public class UserService
{
    public UserRecord GetUser(Guid userId)
    {
        return new UserRecord
        {
            Id = userId,
            FistName = "John",
            LastName = "Doe",
            Email = "john.doe@test.com"
        };
    }

    public ReadOnlyCollection<UserRecord> GetUsers()
    {
        return new ReadOnlyCollection<UserRecord>(new List<UserRecord>
        {
            new()
            {
                Id = new Guid("6EAEA9BB-8094-44FD-8124-6A3D809D7983"),
                FistName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com"
            },
            new()
            {
                Id = new Guid("6EAEA9BB-8094-44FD-8124-6A3D809D7983"),
                FistName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com"
            }
        });
    }

    public Guid SaveUser(UserRecord userRecord)
    {
        return userRecord.Id ?? Guid.NewGuid();
    }
}