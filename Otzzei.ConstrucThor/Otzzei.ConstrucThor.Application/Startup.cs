using Microsoft.AspNetCore.Identity;

namespace Otzzei.ConstrucThor.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("MinhaParoquiaDB");
            services.AddDbContext<ConstrucThorContext>(opt => opt.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(
                opt => opt.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<ConstrucThorContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserService, UserService>();


            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

        }
    }

    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(IApplicationBuilder app, IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);

    }
}

