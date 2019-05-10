using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using utcAltkomDevices.DbServices;
using utcAltkomDevices.FakeServices;
using utcAltkomDevices.FakeServices.Fakers;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;
using utcAltkomDevices.WebService.Handlers;
using utcAltkomDevices.WebService.Hubs;

namespace utcAltkomDevices.WebService
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Fake services
            services.AddSingleton<IEntityServices<Device>, FakeDeviceService>();
            services.AddSingleton<IEntityServices<Customer>, FakeCustomerService>();
            services.AddSingleton<IUsersService, FakeUsersService>();

            services.AddSingleton<DeviceFaker>();
            services.AddSingleton<CustomerFaker>();

            services.AddSingleton<CustomersHub>();

            //Db services
            //services.AddScoped<IEntityServices<Customer>, DbCustomerServices>();
            //services.AddDbContext<UtcContext>(options => options.UseSqlServer(sqlConnection));

            // Getting configuration data from appsettings.json
            // Switch appsettings file with Env vars, for example: DEVELOPMENT
            var q = Configuration["Quantity"];
            // When its complex data
            var sqlConnection = Configuration["ConnectionStrings:Sql"];

            // Injecting by Ioptions<EntityOptions>
            // Add options support and get a config section as a options representation
            services.AddOptions();
            services.Configure<EntityOptions>(Configuration.GetSection("EntityOptions"));

            //Injecting without IOptions
            //var config = new EntityOptions();
            //Configuration.GetSection("EntityOptions").Bind(config);
            //services.AddSingleton<EntityOptions>();

            // Adds Swagger docs generator 
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Api Name", Version = "1.0" }));

            // Add SignalR (WebSockets) support
            services.AddSignalR();


            //Adds authentication
            services.AddAuthentication("BasicAuthorization")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthorization", null);

            //For BasicAuthentication we add to Header:
            //Authorization=Basic login:p@ss     //(login:p@ss) should be in base64
                                                 //Basic is our authentication type

            services.AddMvc(options => options.RespectBrowserAcceptHeader = true). // "Accept" requests from clients are being parsed
                AddXmlSerializerFormatters().   // Adds xml format support
                SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //https://localhost:5001/swagger/v1/swagger.json
            app.UseSwagger();
            //https://localhost:5001/swagger
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Enable authenthication
            app.UseAuthentication();

            //Set SignalR root
            app.UseSignalR(routes => routes.MapHub<CustomersHub>("/hubs/customers"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }


        //If enviroment value is set to PRODUCTION this will be called
        public void ConfigureProdution(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
