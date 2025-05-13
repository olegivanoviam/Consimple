using Bogus;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.Persistence.Seeding;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly Random _random;
    private readonly int _seed;

    public DatabaseSeeder(ApplicationDbContext context, int? seed = null)
    {
        _context = context;
        _seed = seed ?? 12345; // Consistent seed for reproducible data
        _random = new Random(_seed);
        Randomizer.Seed = new Random(_seed);
    }

    public async Task SeedAsync()
    {
        // Only create database if it doesn't exist
        await _context.Database.MigrateAsync();

        // Only seed if there's no data
        if (!await _context.ProductCategories.AnyAsync())
        {
            // Seed in order to maintain referential integrity
            await SeedProductCategoriesAsync();
            await SeedProductsAsync();
            await SeedCustomersAsync();
            await SeedPurchasesAsync();
        }
    }

    private async Task SeedProductCategoriesAsync()
    {
        var categories = new[]
        {
            "Electronics",
            "Books",
            "Clothing",
            "Home & Garden",
            "Sports",
            "Toys",
            "Food & Beverages",
            "Health & Beauty"
        };

        foreach (var categoryName in categories)
        {
            if (!await _context.ProductCategories.AnyAsync(c => c.Name == categoryName))
            {
                var faker = new Faker<ProductCategory>()
                    .RuleFor(c => c.Name, _ => categoryName)
                    .RuleFor(c => c.Description, f => f.Commerce.ProductDescription());

                var category = faker.Generate();
                await _context.ProductCategories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
        }
    }

    private async Task SeedProductsAsync()
    {
        var categories = await _context.ProductCategories.ToListAsync();
        
        var faker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.SKU, f => f.Commerce.Ean13())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
            .RuleFor(p => p.Category, f => f.PickRandom(categories));

        var products = faker.Generate(100);

        await _context.Products.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }

    private async Task SeedCustomersAsync()
    {
        var faker = new Faker<Customer>()
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past(50).Date)
            .RuleFor(c => c.RegistrationDate, f => f.Date.Past(2).Date);

        var customers = faker.Generate(50);

        await _context.Customers.AddRangeAsync(customers);
        await _context.SaveChangesAsync();
    }

    private async Task SeedPurchasesAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        var products = await _context.Products.ToListAsync();
        var startDate = DateTime.UtcNow.AddDays(-100);

        var orderNumber = 1;
        var purchaseFaker = new Faker<Purchase>()
            .RuleFor(p => p.Number, f => $"ORD-{orderNumber++:D6}") // Sequential 6-digit numbers
            .RuleFor(p => p.Date, f => f.Date.Between(startDate, DateTime.UtcNow))
            .RuleFor(p => p.Customer, f => f.PickRandom(customers));

        var itemFaker = new Faker<PurchaseItem>()
            .RuleFor(pi => pi.Product, f => f.PickRandom(products))
            .RuleFor(pi => pi.Quantity, f => f.Random.Int(1, 5))
            .RuleFor(pi => pi.UnitPrice, (f, pi) => pi.Product.Price);

        // Generate more purchases to ensure good distribution in the last 100 days
        var purchases = purchaseFaker.Generate(300)
            .Select(p =>
            {
                var items = itemFaker.Generate(_random.Next(1, 5));
                p.Items = items;
                p.TotalAmount = items.Sum(i => i.Quantity * i.UnitPrice);
                return p;
            })
            .OrderBy(p => p.Date); // Order purchases by date

        await _context.Purchases.AddRangeAsync(purchases);
        await _context.SaveChangesAsync();
    }
} 