using DotNet6API_Sample.Library.Interfaces;
using DotNet6API_Sample.Library.Models;

namespace DotNet6API_Sample.Library.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserRecord? GetUser(Guid userId)
    {
        return _userRepository.ReadByUserId(userId);
    }

    public IReadOnlyCollection<UserRecord> GetUsers()
    {
        return _userRepository.ReadAll();
    }

    public Guid SaveUser(UserRecord userRecord)
    {
        return userRecord.ID == Guid.Empty || userRecord.ID == null
            ? _userRepository.Create(userRecord)
            : _userRepository.Update(userRecord);
    }
}