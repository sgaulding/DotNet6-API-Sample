using DotNet6API_Sample.Library.Models;

namespace DotNet6API_Sample.Library.Interfaces;

public interface IUserRepository
{
    UserRecord ReadByUserId(Guid userRecordId);
    IReadOnlyCollection<UserRecord> ReadAll();
    Guid Create(UserRecord userRecord);
    Guid Update(UserRecord userRecord);
}