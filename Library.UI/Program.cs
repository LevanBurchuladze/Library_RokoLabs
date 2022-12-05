using AutoMapper;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.Repository;
using Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    IConfiguration configuration = hostingContext.Configuration;
                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MappingProfile());
                    });

                    IMapper mapper = mappingConfig.CreateMapper();

                    services.AddSingleton(mapper);
                    services.AddSingleton(new DbOptions() { ConnectionString = connectionString });

                    services.AddTransient<IEditionService, EditionService>();
                    services.AddTransient<IEditionRepository, EditionRepository>();

                    services.AddTransient<IAuthorRepository, AuthorRepository>();

                    services.AddTransient<IBookRepository, BookRepository>();

                    services.AddTransient<INewsPaperRepository, NewsPaperRepository>();

                    services.AddTransient<IPatentRepository,PatentRepository>();

                    services.AddHostedService<Worker>();
                });
        }
    }
}
