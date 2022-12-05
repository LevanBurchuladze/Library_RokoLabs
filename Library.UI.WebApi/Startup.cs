
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Library.Interfaces.Service;
using Library.Services;
using Library.Interfaces.Repository;
using Library.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Library.UI.WebApi.infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Library.UI.WebApi
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUEDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
              
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

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

            services.AddTransient<INewsPaperService, NewsPaperService>();

            services.AddTransient<IPatentRepository, PatentRepository>();

            services.AddTransient<IPatentService, PatentService>();

            services.AddScoped<AuditLogActionFilter>();

            services.AddScoped<ExceptionFilter>();

            services.AddControllersWithViews(options => {
                options.Filters.Add<ExceptionFilter>();
            });

            services.AddCors();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.UI.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.UI.WebApi v1"));
            }

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseDurationLogger();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
