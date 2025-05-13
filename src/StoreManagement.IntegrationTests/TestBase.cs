using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Infrastructure.Persistence;
using StoreManagement.Infrastructure.Persistence.Seeding;

namespace StoreManagement.IntegrationTests;

public abstract class TestBase
{
    protected ApplicationDbContext Context;
    protected DatabaseSeeder Seeder;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreManagement_Tests;Trusted_Connection=True;TrustServerCertificate=true")
            .Options;

        Context = new ApplicationDbContext(options);
        Seeder = new DatabaseSeeder(Context);
    }

    [SetUp]
    public async Task SetUp()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.MigrateAsync();
        await Seeder.SeedAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Context.DisposeAsync();
    }
} 