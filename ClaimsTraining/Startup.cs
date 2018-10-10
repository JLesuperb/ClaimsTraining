using System.Text;
using ClaimsTraining.Data;
using ClaimsTraining.Helpers;
using ClaimsTraining.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace ClaimsTraining
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection _Services)
        {
            _Services.AddCors();

            _Services.AddMvc().AddJsonOptions(_Options => _Options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //For create Database
            _Services.AddDbContext<DefaultContext>(_Options =>
                _Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                _B => _B.MigrationsAssembly("ClaimsTraining")));

            //_Services.AddIdentity<Customer, Role>()
            //    .AddEntityFrameworkStores<DefaultContext>()
            //    .AddDefaultTokenProviders();

            var _AppSettingsSection = Configuration.GetSection("AppSettings");
            _Services.Configure<AppSettings>(_AppSettingsSection);

            // configure jwt authentication
            var _AppSettings = _AppSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(_AppSettings.Secret);

            _Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            _Services.AddScoped<ICustomerService, CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
