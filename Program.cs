// See https://aka.ms/new-console-template for more information
using ConsoleApp5.Data;


using var context = new AppDbContext();

SeedData.Seed(context);

Console.WriteLine("Database seeded successfully!");