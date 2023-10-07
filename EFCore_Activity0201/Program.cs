using EFCore_DBLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_Activity0201
{
    internal class Program
    {
        private static IConfigurationRoot? _configuration;
        private static DbContextOptionsBuilder<AdventureWorks2022Context>? _optionsBuilder;
        static void Main(string[] args)
        {
            BuildConfiguration();
           // Console.WriteLine($"CNSTR: {_configuration.GetConnectionString("AdventureWorks")}");
            BuildOptions();
            ListPeople();
        }
        static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
        static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorks2022Context>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
        }
        static void ListPeople()
        {
            using (var db = new AdventureWorks2022Context(_optionsBuilder.Options))
            {
                var people = db.People.OrderByDescending(x => x.LastName).Take(20).ToList();
                foreach (var person in people)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
    }
}