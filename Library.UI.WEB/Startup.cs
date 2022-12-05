using AutoMapper;
using Library.Interfaces.Repository;
using Library.Interfaces.Service;
using Library.Repository;
using Library.Services;
using Library.UI.WEB.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.UI.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Login";
                });
            services.AddAuthorization();

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

            services.AddTransient<IBookService, BookService>();

            services.AddTransient<IAuthorService, AuthorService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<INewsPaperRepository, NewsPaperRepository>();

            services.AddTransient<IPatentRepository, PatentRepository>();

            services.AddTransient<IPatentService, PatentService>();

            services.AddScoped<AuditLogActionFilter>();

            services.AddScoped<ExceptionFilter>();

            services.AddControllersWithViews(options => {
                options.Filters.Add<ExceptionFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

/*            app.Use(async (context, next) =>
            {
                var apiKey = context.Request.Headers["ApiKey"];
                if (!(apiKey.Count == 1 && apiKey[0] == "euqg31hpq8rvun"))
                {
                    await context.Response.WriteAsync("No valid Apikey found");
                }
                else
                {
                    await next.Invoke();
                }
            });*/

            app.UseHttpsRedirection();

            app.UseDurationLogger();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Edition}/{action=Index}/{id?}");
            });
        }
    }
}
