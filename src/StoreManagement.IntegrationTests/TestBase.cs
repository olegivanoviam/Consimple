using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Infrastructure.Persistence;
using StoreManagement.Infrastructure.Persistence.Seeding;

namespace StoreManagement.IntegrationTests;

public abstract class TestBase
{
    protected ApplicationDbContext Context { get; private set; } = null!;
    protected DatabaseSeeder Seeder { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreManagement_Tests;Trusted_Connection=True;TrustServerCertificate=true")
            .Options;

        Context = new ApplicationDbContext(options);
        Seeder = new DatabaseSeeder(Context);

        await Context.Database.EnsureDeletedAsync();
        await Context.Database.MigrateAsync();
        await Seeder.SeedAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (Context != null)
        {
            await Context.Database.EnsureDeletedAsync();
        }
    }
} 