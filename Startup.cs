using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using keepr_final.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace keepr_final
{
    public class Startup
    {
        private readonly string _connetionString = "host=sql3.freemysqlhosting.net;port=3306;database=sql3232300;user id=sql3232300;password=D6hlDmuSfY;Allow User Variables=True;";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // _connetionString = configuration.GetSection("DB").GetValue<string>("mySQLConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login/";
                options.Events.OnRedirectToLogin = (context) => //context is node equivelant of res
                {
                    context.Response.StatusCode = 401; // if login fails
                    return Task.CompletedTask;
                };
            });

            services
            .AddCors(options =>
            {
                //ONLY IN DEVELOPMENT MODE IE LOCALHOST:8080
                options.AddPolicy("CORS_ENVIROMENT_IS_DEVELOPMENT", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                }); 
            });
            // END AUTH. ABOVE ^^^ IS COPY AND PASTE ABOUT 99% OF THE TIME.

            services.AddMvc();
            services.AddTransient<IDbConnection>(x => CreateDBContext());
            services.AddTransient<UserRepository>();
            services.AddTransient<KeepRepository>();
            services.AddTransient<BoardRepository>();
        }

        private IDbConnection CreateDBContext()
        {
            var connection = new MySqlConnection(_connetionString);
            connection.Open();
            return connection;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
                app.UseCors("CORS_ENVIROMENT_IS_DEVELOPMENT"); // IN DEVELOPMENT HERE. MOVE DOWN FOR 
            }
            // GIVES ACCESS TO WWWROOT FOLDER FILES
            // USEDEFAULTFILES NEEDS TO GO FIRST!!! GIVE ACCESS TO VUE PROJECT
            app.UseDefaultFiles(); // PRIORITY TO INDEX.HTML. IE IT WILL AUTOLOAD AS A DEFAULT
            app.UseStaticFiles();

            app.UseAuthentication(); // IMPLEMENTS AUTHENTICATION FROM ABOVE
            app.UseMvc();
        }
    }
}
