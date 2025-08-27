using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Million.Domain.Entities;
using Million.Infrastructure.Persistence;
using Million.TestUserCreator;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IdentitySeeder>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
await seeder.SeedAsync();