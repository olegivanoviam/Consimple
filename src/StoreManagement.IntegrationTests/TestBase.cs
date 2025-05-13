using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.Persistence;
using StoreManagement.Infrastructure.Persistence.Seeding;

namespace StoreManagement.IntegrationTests;

public abstract class TestBase : IAsyncLifetime
{
    protected readonly ApplicationDbContext Context;
    protected readonly DatabaseSeeder Seeder;

    protected TestBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreManagement_Tests;Trusted_Connection=True;TrustServerCertificate=true")
            .Options;

        Context = new ApplicationDbContext(options);
        Seeder = new DatabaseSeeder(Context);
    }

    public async Task InitializeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.MigrateAsync();
        await Seeder.SeedAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
    }
} 