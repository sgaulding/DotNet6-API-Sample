using DotNet6API_Sample.Library.Interfaces;
using DotNet6API_Sample.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet6API_Sample.Library.Repositories;

public class EntityFrameworkUserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;

    public EntityFrameworkUserRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public UserRecord? ReadByUserId(Guid userRecordId)
    {
        return _userDbContext.Find<UserRecord>(userRecordId);
    }

    public IReadOnlyCollection<UserRecord> ReadAll()
    {
        return _userDbContext.DbSetUsers?.ToList() ?? new List<UserRecord>();
    }

    public Guid Create(UserRecord userRecord)
    {
        var newUserRecord = _userDbContext.Add(userRecord);
        _userDbContext.SaveChanges();
        return newUserRecord.Entity.ID ?? Guid.Empty;
    }

    public Guid Update(UserRecord userRecord)
    {
        var recordToUpdate = _userDbContext.Find<UserRecord>(userRecord.ID);
        if (recordToUpdate == null) return Guid.Empty;

        _userDbContext.Entry(recordToUpdate).CurrentValues.SetValues(userRecord);
        _userDbContext.SaveChanges();

        return recordToUpdate.ID ?? Guid.Empty;
    }
}

public class UserDbContext : DbContext
{
    private readonly string _connectionString;

    public UserDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<UserRecord>? DbSetUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserRecord>().ToTable("users");
    }
}